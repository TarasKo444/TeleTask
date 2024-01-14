using Deployf.Botf;
using MediatR;
using Microsoft.Extensions.Options;
using TeleTask.Bot.Host.Controllers.Schedule;
using TeleTask.Bot.Host.Options;
using TeleTask.Infrastructure;

namespace TeleTask.Bot.Host.Controllers.Main;

public partial class MainController : BotController
{
    private readonly BotDbContext _dbContext;
    private readonly ILogger<MainController> _logger;
    private readonly IOptions<TelegramOptions> _telegramOptions;
    private readonly ISender _sender;

    public MainController(BotDbContext dbContext, ILogger<Main.MainController> logger, IOptions<TelegramOptions> telegramOptions, ISender sender)
    {
        _dbContext = dbContext;
        _logger = logger;
        _telegramOptions = telegramOptions;
        _sender = sender;
    }

    [Action("/start", "Starts bot")]
    public void Start()
    {
        PushL("Welcome, choose action");
        
        Button("Add", Q<ScheduleController>(c => c.StartAddProcess));   
        Button("Show", Q<ScheduleController>(c => c.Show));
        Button("End", Q<ScheduleController>(c => c.StartEndProcess));
        
        if (FromId == _telegramOptions.Value.Admin)
        {
            RowButton("Admin", Q(AdminButton));
        }
    }
}
