using Hangfire;
using Hangfire.MemoryStorage;
using NLog.Web;
using OvdVsBotWeb.Handlers;
using OvdVsBotWeb.Jobs;
using OvdVsBotWeb.Models.API.Commands;
using OvdVsBotWeb.Models.API.Commands.Processors;
using OvdVsBotWeb.Models.Commands;
using OvdVsBotWeb.ResourceManagement;
using OvdVsBotWeb.Services;
using OvdVsBotWeb.Settings;
using Telegram.Bot;
using Telegram.Bot.Polling;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<BotSettings>(builder.Configuration.GetSection(nameof(BotSettings)));
var botConfig = new BotSettings();
builder.Configuration.GetSection(nameof(BotSettings)).Bind(botConfig);

builder.Services
    .AddSingleton<ITelegramBotClient, TelegramBotClient>(tf => new TelegramBotClient(botConfig.TelegramToken))
    .AddSingleton<CommandProcessorFactory>()
    .AddScoped<CommandProcessor<Start>, StartCommandProcessor>()
    .AddScoped<CommandProcessor<CreateSchedule>, CreateScheduleCommandProcessor>()
    .AddScoped<CommandProcessor<RemoveSchedule>, RemoveScheduleCommandProcessor>()
    .AddScoped<CommandProcessor<Unknown>, UnknownCommandProcessor>()
    .AddSingleton<IJobManager, JobManager>()
    .AddSingleton<MessageTextManager>()
    .AddSingleton<IUpdateHandler, BotUpdateHandler>()
    .AddSingleton<JobManager>()
    .AddHostedService<BotService>()
    .AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseMemoryStorage());

builder.Host.ConfigureLogging(logging =>
                                {
                                    logging.ClearProviders();
                                    logging.SetMinimumLevel(LogLevel.Trace);
                                    logging.AddConsole();
                                })
    .UseNLog();

var app = builder.Build();

app.Run();