using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;

namespace GMM7
{
    public class IFFmpegTest
    {
        IConfiguration Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();

        ILogger<DefaultFFmpeg> Logger;
        IFFfmpegConverter FFmpeg;

        public IFFmpegTest()
        {
            Logger = LoggerFactory.Create(config =>
            {
                config.AddConsole();
            }).CreateLogger<DefaultFFmpeg>();
            FFmpeg = new DefaultFFmpeg(Logger, Configuration);
        }

        [Fact]
        public async void ConvertMp4ToM3u8()
        {
            var bastPath = "";
            var source = "";
            var target = "";
            await FFmpeg.StartConvertTaskAsync(Path.GetFullPath(source), Path.GetFullPath(target));
        }

        [Fact]
        public async void ExtractImage()
        {
            var bastPath = "";
            var timeSpan = new TimeSpan(0, 1, 50);
            var source = "";
            var target = "";
            var base64 = await FFmpeg.ExtractImageAsync(Path.GetFullPath(source), target, timeSpan);
            Assert.NotEmpty(base64);
        }

        [Fact]
        public async void Test2()
        {
            var taskId = "";
            FFmpeg.StopConvert(taskId);
        }
    }
}