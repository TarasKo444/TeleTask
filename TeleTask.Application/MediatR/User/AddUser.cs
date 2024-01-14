using MediatR;
using Microsoft.Extensions.Logging;
using TeleTask.Infrastructure;

namespace TeleTask.Application.MediatR.User;

public static class AddUser
{
    public sealed class Request : IRequest
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
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
            if (!_dbContext.Users.Any(u => u.Id == request.Id))
            {
                var user = new Domain.User
                {
                    Id = request.Id,
                    Username = request.Username,
                };

                await _dbContext.Users.AddAsync(user, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            
                _logger.LogInformation("Added new user {id} @{username}", user.Id, user.Username);
            }
        }
    }
}
