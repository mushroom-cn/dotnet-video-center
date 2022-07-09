using System.Reflection;
using GMM7;
using GMM7.Jobs;
using GMM7.Models;
using GMM7.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Quartz;
using Serilog;

var AllowSpecificOrigins = "Gmm7AllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json")
      .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
      .Build();

// #region config serilog start
var seriLog = new LoggerConfiguration()
// .MinimumLevel.Debug()
          .ReadFrom.Configuration(configuration, sectionName: "Serilog")
          //   .Enrich.FromLogContext()
          //   .WriteTo.Console()
          .CreateLogger();
Log.Logger = seriLog;
ILoggerFactory loggerFactory = LoggerFactory.Create(logging =>
{
    logging.AddSerilog(seriLog);
});
builder.Host.UseSerilog();
builder.Services.AddSingleton(loggerFactory);
// #endregion

// #region config quartz
builder.Services.Configure<QuartzOptions>(configuration.GetSection("Quartz"));
builder.Services.AddQuartz((q) =>
{
    q.SchedulerId = "Scheduler-Core";
    q.UseMicrosoftDependencyInjectionJobFactory();
    q.ScheduleJob<FileScannerJob>(trigger => trigger
         .WithIdentity("Combined Configuration Trigger")
         .StartNow()
         .WithSimpleSchedule(x =>
         x.WithIntervalInHours(1).RepeatForever()
         )
     );
    // auto-interrupt long-running job
    q.UseJobAutoInterrupt(options =>
    {
        // this is the default
        options.DefaultMaxRunTime = TimeSpan.FromMinutes(60);
    });
});

builder.Services.AddQuartzHostedService(options =>
                {
                    // when shutting down we want jobs to complete gracefully
                    options.WaitForJobsToComplete = true;
                });
// #endregion

// #region cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:44480",
                                              "https://127.0.0.1:44480")
                                              .AllowAnyHeader()
                                                  .AllowAnyMethod();
                      });
});

// #endregion

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Media And Tag API",
        Description = "Manage your local media files.",
        Contact = new OpenApiContact
        {
            Name = "mushroom-cn",
            Url = new Uri("https://github.com/mushroom-cn/dotnet-video-center")
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://github.com/mushroom-cn/dotnet-video-center/blob/main/LICENSE")
        }
    });
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddScoped<IDBContext, DefaultDBContext>();
builder.Services.AddScoped<IEntityService<Tag>, DefaultTagServices>();
builder.Services.AddScoped<IEntityService<Media>, DefaultMediaServices>();
builder.Services.AddScoped<IRelationService<MediaTag>, DefaultMediaTagServices>();
builder.Services.AddScoped<IFFfmpegConverter, DefaultFFmpeg>();
builder.Services.AddScoped<IFileScannerService, DefaultFileScannerService>();
// builder.Services.AddSingleton<SchedulerJob>();
// builder.Services.AddHostedService<DefaultHostedSchedulerService>();
builder.Services.AddDirectoryBrowser();

var app = builder.Build();
app.UseSerilogRequestLogging();

// #region static resources
var provider = new FileExtensionContentTypeProvider();
// Add new mappings
provider.Mappings[".m3u8"] = "application/x-mpegURL";
// app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
    ContentTypeProvider = provider
});
var ffmpegTargetDir = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, configuration["FFmpeg:Target"]));
if (!Directory.Exists(ffmpegTargetDir))
{
    Directory.CreateDirectory(ffmpegTargetDir);
}
var opt = new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(ffmpegTargetDir),
    RequestPath = "/static/m3u8",
    EnableDirectoryBrowsing = true,
};
opt.StaticFileOptions.ContentTypeProvider = provider;
app.UseFileServer(opt);
// #endregion

// #region start app
app.UseExceptionHandler("/Error");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}



// #region static resources
app.UseRouting();
app.UseCors(AllowSpecificOrigins);
app.MapControllerRoute(
    name: "DefaultApi",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
// #endregion