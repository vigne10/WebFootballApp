using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;

namespace WebFootballApp.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> User { get; set; }
    public DbSet<Article> Article { get; set; }
    public DbSet<Competition> Competition { get; set; }
    public DbSet<Team> Team { get; set; }
    public DbSet<Position> Position { get; set; }
    public DbSet<Player> Player { get; set; }
    public DbSet<Job> Job { get; set; }
    public DbSet<StaffMember> StaffMember { get; set; }
    public DbSet<Match> Match { get; set; }
    public DbSet<Season> Season { get; set; }
    public DbSet<Ranking> Ranking { get; set; }
    public DbSet<Reward> Reward { get; set; }
    public DbSet<CompetitionSeason> CompetitionSeason { get; set; }
    public DbSet<CompetitionSeasonTeam> CompetitionSeasonTeam { get; set; }
    public DbSet<History> History { get; set; }
}