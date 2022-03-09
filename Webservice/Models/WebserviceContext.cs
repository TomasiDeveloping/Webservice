using Microsoft.EntityFrameworkCore;
using Webservice.Helper;

namespace Webservice.Models;

public class WebserviceContext : DbContext
{
    public WebserviceContext(DbContextOptions<WebserviceContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<LogType> LogTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().Property(u => u.UserName).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<User>().Property(u => u.Role).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
        modelBuilder.Entity<User>().Property(u => u.CreatedAt).IsRequired();

        modelBuilder.Entity<Log>().Property(l => l.Requester).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<Log>().Property(l => l.RequestDate).IsRequired();
        modelBuilder.Entity<Log>().Property(l => l.RequestMethod).HasMaxLength(150).IsRequired();
        modelBuilder.Entity<Log>()
            .HasOne(l => l.LogType)
            .WithMany()
            .HasForeignKey(l => l.LogTypeId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<LogType>().Property(lt => lt.Name).HasMaxLength(150).IsRequired();
        modelBuilder.Entity<LogType>().HasData(
            new LogType {Id = (int) Constantes.LogTypes.SUCCESS, Name = Constantes.LogTypes.SUCCESS.ToString()},
            new LogType {Id = (int) Constantes.LogTypes.ERROR, Name = Constantes.LogTypes.ERROR.ToString()},
            new LogType {Id = (int) Constantes.LogTypes.INSERT, Name = Constantes.LogTypes.INSERT.ToString()},
            new LogType {Id = (int) Constantes.LogTypes.UPDATE, Name = Constantes.LogTypes.UPDATE.ToString()},
            new LogType {Id = (int) Constantes.LogTypes.DELETE, Name = Constantes.LogTypes.DELETE.ToString()}
        );
    }
}