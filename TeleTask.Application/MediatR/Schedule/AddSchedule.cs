using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using TeleTask.Infrastructure;

namespace TeleTask.Application.MediatR.Schedule;

public static class AddSchedule
{
    public sealed class Request : IRequest
    {
        public string Name { get; set; } = null!;
        public DateTime Deadline { get; set; }
        public long UserId { get; set; }
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
            if (_dbContext.Users.Any(u => u.Id == request.UserId))
            {
                var schedule = request.Adapt<Domain.Schedule>();

                await _dbContext.Schedules.AddAsync(schedule, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Schedult created with id {schedule.Id}");
            }
        }
    }
}