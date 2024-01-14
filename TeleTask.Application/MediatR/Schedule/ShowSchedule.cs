using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeleTask.Infrastructure;

namespace TeleTask.Application.MediatR.Schedule;

public class ShowSchedule
{
    public sealed class Request : IRequest<List<ScheduleResponse>>
    {
        public long UserId { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Request, List<ScheduleResponse>>
    {
        private readonly BotDbContext _dbContext;
        private readonly ILogger<Handler> _logger;

        public Handler(BotDbContext dbContext, ILogger<Handler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<ScheduleResponse>> Handle(Request request, CancellationToken cancellationToken)
        {
            if (_dbContext.Users.Any(u => u.Id == request.UserId))
            {
                return Task.FromResult(_dbContext.Schedules
                    .OrderBy(s => s.Deadline)
                    .AsNoTracking()
                    .ProjectToType<ScheduleResponse>()
                    .ToList());
            }

            return Task.FromResult(new List<ScheduleResponse>());
        }
    }
}
