using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeleTask.Infrastructure;

namespace TeleTask.Application.MediatR.Schedule;

public class EndSchedule
{
    public sealed class Request : IRequest
    {
        public long Id { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Request>
    {
        private readonly BotDbContext _dbContext;
        private readonly ILogger<Handler> _logger;

        public Handler(BotDbContext dbContext, ILogger<Handler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task Handle(Request request, CancellationToken cancellationToken)
        {
            var schedule = await _dbContext.Schedules.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
            
            if (schedule is not null)
            {
                _dbContext.Schedules.Remove(schedule);
                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Schedult deleted with id {schedule.Id}");
            }
        }
    }
}
