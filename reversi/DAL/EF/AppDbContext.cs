using System.Text.Json;
using BLL;
using DAL.DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public class AppDbContext : DbContext
{
    public DbSet<GameConfigDto> Configurations { get; set; }
    public DbSet<GameStateDto> GameStates { get; set; }
    public DbSet<PlayerDto> Players { get; set; }

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameStateDto>()
            .Property(g => g.Board)
            .HasConversion(
                board => JsonSerializer.Serialize(board, DalHelpers.JsonSerializerOptions),
                json => JsonSerializer.Deserialize<ECellPiece[][]>(
                    json, DalHelpers.JsonSerializerOptions)!
            );

        modelBuilder.Entity<GameStateDto>()
            .HasOne(g => g.Config)
            .WithMany(c => c.GameStates)
            .OnDelete(DeleteBehavior.Cascade);
    }
}