using Microsoft.EntityFrameworkCore;
using Performance_test_csharp.Models;

namespace Performance_test_csharp.Data;

public class AppDbcontext : DbContext
{
    // Constructor que recibe las options de config
    public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
    {
    }
    // le Dbsets (representaciones de tables en la db)
    public DbSet<User> Users => Set<User>();
    public DbSet<DeportiveSpace> DeportiveSpaces => Set<DeportiveSpace>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    
    //On model creating (aqui se configura el fluent api)
    // todo: para los temas de validacion de datos en db (definimos relaciones y demas cositas wiwiwi)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //config para el user
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Document)
            .IsUnique();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        //config para los espacios
        modelBuilder.Entity<DeportiveSpace>()
            .HasIndex(d => d.Name)
            .IsUnique();
        
        //config para reservas (relations)
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.SportSpace)
            .WithMany(d => d.Reservations) //conectamos con lista (la tipo ICollection) de Deportive space 
            .HasForeignKey(r => r.SpaceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}