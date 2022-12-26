using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.Handlers;
using OvdVsBotWeb.Jobs;
using OvdVsBotWeb.Models.API.Commands;
using OvdVsBotWeb.Models.API.Commands.Processors;
using OvdVsBotWeb.Models.API.Commands.Validators;
using OvdVsBotWeb.Models.Commands;
using OvdVsBotWeb.Models.Data;
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
   .AddSingleton<CommandProcessor<Lang>, LangCommandProcessor>()
   .AddSingleton<CommandProcessor<Start>, StartCommandProcessor>()
   .AddSingleton<CommandProcessor<Stop>, StopCommandProcessor>()
   .AddSingleton<CommandProcessor<CreateSchedule>, CreateScheduleCommandProcessor>()
   .AddSingleton<CommandProcessor<RemoveSchedule>, RemoveScheduleCommandProcessor>()
   .AddSingleton<CommandProcessor<Unknown>, UnknownCommandProcessor>()
   .AddSingleton<IReadWriter<Chat, string>, SqliteChatRepository>()
   .AddSingleton<IJobManager, JobManager>()
   .AddSingleton<MessageTextManager>()
   .AddSingleton<IUpdateHandler, BotUpdateHandler>()
   .AddSingleton<JobManager>()
   .AddSingleton<SendMessageJob>()
   .AddSingleton<IJobManagementService, JobManagementService>()
   .AddSingleton<ICommandValidator<Start>, PassValidator<Start>>()
   .AddSingleton<ICommandValidator<Stop>, PassValidator<Stop>>()
   .AddSingleton<ICommandValidator<Unknown>, PassValidator<Unknown>>()
   .AddSingleton<ICommandValidator<RemoveSchedule>, PassValidator<RemoveSchedule>>()
   .AddSingleton<ICommandValidator<CreateSchedule>, PassValidator<CreateSchedule>>()
   .AddSingleton<ICommandValidator<Lang>, LangValidator>()
   .AddSingleton(sp => new RandomSendMessageJob(sp.GetRequiredService<ITelegramBotClient>(),
                                                sp.GetRequiredService<ILogger<SendMessageJob>>(),
                                                15))
   .AddHostedService<BotService>()
   .AddHangfire(configuration => configuration
       .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
       .UseSimpleAssemblyNameTypeSerializer()
       .UseRecommendedSerializerSettings()
       .UseMemoryStorage())
   .AddHangfireServer()
   .AddDbContext<OvdDbContext>(o => o.UseSqlite(botConfig.ConnectionString))
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
