using BLL;
using DAL.DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public class AppDbContext : DbContext
{
    public DbSet<GameConfiguration> Configs { get; set; }
    public DbSet<GameStateDto> States { get; set; }

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        optionsBuilder.UseSqlite(DefaultConnectionString());
    }

    public static string DefaultConnectionString()
    {
        var folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            ".icd0008",
            "reversi-othello");
        Directory.CreateDirectory(folder);
        return $"Data Source={Path.Combine(folder, "app.db")}";
    }
}