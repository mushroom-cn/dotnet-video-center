using GMM7.Models;
using GMM7.Services;
using Quartz;

namespace GMM7.Jobs
{
    public class FileScannerJob : IJob, IDisposable
    {
        private readonly ILogger<FileScannerJob> Logger;
        private readonly string ID = Guid.NewGuid().ToString();
        private readonly IConfiguration Configuration;
        private readonly IEntityService<Media> MediaService;
        IFFfmpegConverter FFmpegConverter;

        private readonly IFileScannerService FileScannerService;
        public FileScannerJob(
            ILogger<FileScannerJob> logger,
            IConfiguration configuration,
            IFileScannerService fileScannerService)
        {
            Logger = logger;
            Configuration = configuration;
            FileScannerService = fileScannerService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var targetDir = Configuration["FFmpeg:Target"];
            var sourceDir = Path.GetFullPath(Configuration["FFmpeg:Source"]);
            var searchOptions = Configuration["FFmpeg:SearchOption"].Split(",");
            await FileScannerService.Scan(sourceDir, targetDir, searchOptions);
        }

        public void Dispose()
        {
            Logger.LogInformation($"{ID} Dispose task");
            Logger.LogDebug($"{ID} Dispose task");
            Logger.LogError($"{ID} Dispose task");

        }
    }
}