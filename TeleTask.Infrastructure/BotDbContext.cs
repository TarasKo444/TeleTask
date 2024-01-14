using Microsoft.EntityFrameworkCore;
using TeleTask.Domain;

namespace TeleTask.Infrastructure;

public class BotDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Schedule> Schedules { get; set; } = null!;

    public BotDbContext(DbContextOptions options) : base(options)
    {
    }
}
