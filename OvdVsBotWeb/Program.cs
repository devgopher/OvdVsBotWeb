using Hangfire;
using Hangfire.MemoryStorage;
using NLog.Web;
using OvdVsBotWeb.DataAccess;
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
    .AddSingleton<CommandProcessor<Start>, StartCommandProcessor>()
    .AddSingleton<CommandProcessor<CreateSchedule>, CreateScheduleCommandProcessor>()
    .AddSingleton<CommandProcessor<RemoveSchedule>, RemoveScheduleCommandProcessor>()
    .AddSingleton<CommandProcessor<Unknown>, UnknownCommandProcessor>()
    .AddSingleton<IReadWriter<string>, MemoryRepository<string>>()
    .AddSingleton<IJobManager, JobManager>()
    .AddSingleton<MessageTextManager>()
    .AddSingleton<IUpdateHandler, BotUpdateHandler>()
    .AddSingleton<JobManager>()
    .AddSingleton<SendMessageJob>()
    .AddSingleton(sp => new RandomSendMessageJob(sp.GetRequiredService<ITelegramBotClient>(),
                                                 sp.GetRequiredService<ILogger<SendMessageJob>>(),
                                                 10))
    .AddHostedService<BotService>()
    .AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseMemoryStorage())
    .AddHangfireServer()
    .AddMvc();

builder.Host.ConfigureLogging(logging =>
                                {
                                    logging.ClearProviders();
                                    logging.SetMinimumLevel(LogLevel.Trace);
                                    logging.AddConsole();
                                })
    .UseNLog();


var app = builder.Build();
app.UseHangfireDashboard();

app.Run();
