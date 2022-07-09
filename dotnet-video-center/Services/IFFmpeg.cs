using System;
using System.Collections.Concurrent;
using System.Text.Json;
using Xabe.FFmpeg;

namespace GMM7
{
    public class FFmpegConvertCompleteEvent : EventArgs
    {

    }
    public class FFmpegConvertResult
    {
        public string TaskId { get; }
        public FFmpegConvertResult(string traceId)
        {
            TaskId = traceId;
        }
        public event EventHandler<FFmpegConvertCompleteEvent> OnFFmpegConvertResultCompleteHandler;

        public void Invoke<T>(T t, FFmpegConvertCompleteEvent evn)
        {
            OnFFmpegConvertResultCompleteHandler.Invoke(t, evn);
        }
    }

    public interface IFFfmpegConverter
    {
        Task<string> StartConvertTaskAsync(string source, string target);

        FFmpegConvertResult StartConvertTask(string source, string target);

        int GetTaskPercent(string taskId);

        void StopConvert(string taskId);

        Task<string> ExtractImageAsync(string path, string target, TimeSpan timeSpan);

    }

    public class DefaultFFmpeg : IFFfmpegConverter
    {
        private readonly ILogger<DefaultFFmpeg> Logger;
        private readonly ConcurrentDictionary<string, CancellationTokenSource> TokenCache;
        private readonly ConcurrentDictionary<string, int> PercentCache;
        private readonly ConcurrentDictionary<string, string> SourceCache;

        private readonly IConfiguration Configuration;

        public DefaultFFmpeg(ILogger<DefaultFFmpeg> logger, IConfiguration configuration)
        {
            Configuration = configuration;
            Logger = logger;
            TokenCache = new ConcurrentDictionary<string, CancellationTokenSource>();
            PercentCache = new ConcurrentDictionary<string, int>();
            SourceCache = new ConcurrentDictionary<string, string>();
        }

        public async Task<string> StartConvertTaskAsync(string source, string target)
        {
            return await StartConvertCore(source, target, Guid.NewGuid().ToString());
        }

        public FFmpegConvertResult StartConvertTask(string source, string target)
        {
            var traceId = Guid.NewGuid().ToString();
            var result = new FFmpegConvertResult(traceId);
            StartConvertCore(source, target, traceId).ContinueWith(async (v) =>
            {
                result.Invoke(this, new FFmpegConvertCompleteEvent());
            });
            return result;
        }

        public async Task<string> ExtractImageAsync(string path, string target, TimeSpan timeSpan)
        {
            var traceId = Guid.NewGuid();
            var imageName = Guid.NewGuid().ToString();
            var imageUrl = Path.GetFullPath(Path.Combine(target, $"{imageName}.png"));
            Logger.LogInformation($"Convert {imageUrl} from {path}");
            // ffmpeg -i input.flv -ss 00:00:14.435 -vframes 1 out.png
            await ConvertCore(path, imageUrl, traceId.ToString(), (conversion) =>
             {
                 conversion.AddParameter($"-ss {timeSpan}")
                 .AddParameter($"-vframes 1");
             });
            if (!File.Exists(imageUrl))
            {
                return string.Empty;
            }
            var imageBytes = File.ReadAllBytes(imageUrl);
            // File.Delete(imageUrl);
            return Convert.ToBase64String(imageBytes);
        }

        public void StopConvert(string taskId)
        {
            TokenCache.TryGetValue(taskId, out var token);
            token?.Cancel();
        }

        private async Task<string> StartConvertCore(string source, string target, string traceId)
        {
            return await ConvertCore(source, Path.GetFullPath(Path.Combine(target, $"{traceId}.m3u8")), traceId, (conversion) =>
           {
               conversion
                     .AddParameter("-c copy")
                     //   .AddParameter("-profile:v high")
                     .AddParameter("-level 5.0")
                     .AddParameter("-start_number 0")
                     .AddParameter("-hls_time 10")
                     .AddParameter($"-f hls")
                     .AddParameter("-hls_list_size 0");
           });
        }

        private async Task<string> ConvertCore(string source, string target, string traceId, Action<IConversion> action)
        {
            if (!File.Exists(source))
            {
                throw new FileNotFoundException($"{source} not found");
            }
            SourceCache.TryGetValue(source, out var isSourceExist);
            if (!string.IsNullOrEmpty(isSourceExist))
            {
                return isSourceExist;
            }
            var commandString = string.Empty;
            try
            {
                var conversion = await CreateConversion(source, traceId.ToString());
                // 支持取消操作
                var cancellationTokenSource = new CancellationTokenSource();
                TokenCache.TryAdd(traceId.ToString(), cancellationTokenSource);
                cancellationTokenSource.Token.Register(() =>
                {
                    Logger.LogDebug($"Stop convert task: {traceId}");
                });
                Logger.LogInformation($"{traceId} convert [{source}] to [{target}]");
                action(conversion);
                commandString = conversion.SetOutput(target).Build();
                var result = await conversion.Start(cancellationTokenSource.Token);
                Logger.LogInformation($"{traceId} complete start from {result.StartTime}, end {result.EndTime}");
                PercentCache.Remove(traceId, out var percent);
                TokenCache.Remove(traceId, out var token);
                return traceId.ToString();
            }
            catch (Exception e)
            {
                Logger.LogDebug(commandString);
                Logger.LogError(e.ToString());
            }
            return string.Empty;
        }

        private async Task<IConversion> CreateConversion(string source, string traceId)
        {
            try
            {
                var exePath = Configuration["FFmpeg:ExePath"];
                Xabe.FFmpeg.FFmpeg.SetExecutablesPath(exePath);
                IMediaInfo mediaInfo = await Xabe.FFmpeg.FFmpeg.GetMediaInfo(source);
                if (mediaInfo.Size < 1)
                {
                    return null;
                }
                var videoStreams = mediaInfo.VideoStreams.FirstOrDefault().SetCodec(VideoCodec.h264);
                var audioStreams = mediaInfo.AudioStreams.FirstOrDefault().SetCodec(AudioCodec.aac);
                var conversion = Xabe.FFmpeg.FFmpeg.Conversions.New();
                conversion.OnProgress += (v, args) =>
                {
                    var percent = (int)(Math.Round(args.Duration.TotalSeconds / args.TotalLength.TotalSeconds, 2) * 100);
                    PercentCache.AddOrUpdate(traceId, (k) => percent, (k, i) => percent);
                    Logger.LogInformation($"traceId: {traceId}, percent: {percent}, process id: {args.ProcessId}, total length: {args.TotalLength}");
                };
                conversion.OnVideoDataReceived += (sender, args) =>
                {
                    Logger.LogInformation($"traceId: {traceId}");
                };
                conversion
                      .AddStream(audioStreams)
                      .AddStream(videoStreams);
                return conversion;
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
            }
            return null;
        }

        public int GetTaskPercent(string taskId)
        {
            throw new NotImplementedException();
        }
    }
}