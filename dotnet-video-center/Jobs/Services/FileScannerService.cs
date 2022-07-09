using GMM7.Models;
using GMM7.Services;
using Quartz;

namespace GMM7.Jobs
{
    public interface IFileScannerService
    {
        Task Scan(string source, string target, IEnumerable<string> searchOptions);
    }

    public class DefaultFileScannerService : IFileScannerService
    {
        private readonly ILogger<DefaultFileScannerService> Logger;
        private readonly string ID = Guid.NewGuid().ToString();
        private readonly IConfiguration Configuration;
        private readonly IEntityService<Media> MediaService;
        IFFfmpegConverter FFmpegConverter;

        public DefaultFileScannerService(
            ILogger<DefaultFileScannerService> logger,
            IConfiguration configuration,
            IEntityService<Media> mediaService,
            IFFfmpegConverter ffmpegConverter
        )
        {
            Logger = logger;
            Configuration = configuration;
            MediaService = mediaService;
            FFmpegConverter = ffmpegConverter;
        }
        public Task Scan(string source, string targetDir, IEnumerable<string> searchOptions)
        {
            var files = Directory.GetFiles(source, "*.*", new EnumerationOptions()
            {
                RecurseSubdirectories = true
            }).Where(x => searchOptions.Any(y => x.EndsWith(y))).Select(x => x);
            if (files.Count() < 1)
            {
                Logger.LogInformation($"No file found in {source} during job with [{ID}].");
                return Task.CompletedTask;
            }
            var entires = MediaService.GetEntitiesById(new List<string>());
            var existEntries = from entry in entires select entry.Path;
            var notExistedFiles = from file in files
                                  where !existEntries.Contains(file)
                                  select file;

            if (notExistedFiles?.Count() < 1)
            {
                return Task.CompletedTask;
            }
            var tasks = new List<Task<string>>();
            var imageTasks = new List<Task<string>>();
            foreach (var file in notExistedFiles)
            {
                tasks.Add(FFmpegConverter.StartConvertTaskAsync(file,
                    Path.GetFullPath(targetDir)
                ));
                imageTasks.Add(FFmpegConverter.ExtractImageAsync(file, targetDir, new TimeSpan(0, 0, 10)));
            }
            Task.WaitAll(imageTasks.ToArray());
            Task.WaitAll(tasks.ToArray());
            var newEntires = new List<Media>();
            var taskIds = from task in tasks select task.Result;
            var images = from task in imageTasks select task.Result;

            foreach (var file in notExistedFiles.Select((v, i) => new { Value = v, Index = i }))
            {
                var fileInfo = new FileInfo(file.Value);
                newEntires.Add(new Media()
                {
                    Path = file.Value,
                    Name = fileInfo.Name,
                    MediaType = "m3u8",
                    Url = taskIds.Where((x, index) => index == file.Index).First(),
                    IconUrl = images.Where((x, index) => index == file.Index).First() ?? string.Empty
                });
            }
            MediaService.CreateEntities(newEntires);
            return Task.CompletedTask;
        }
    }
}