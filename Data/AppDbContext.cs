using System;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class AppDbContext : DbContext
{
    public DbSet<Studio> Studios => Set<Studio>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Actor> Actors => Set<Actor>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.Entity<Department>()
        //     .HasMany(d => d.Employees)
        //     .WithOne(e => e.Department)
        //     .HasForeignKey(e => e.DepartmentId);

        // builder.Entity<Movie>().ToTable("Movie");
        // builder.Entity<Studio>().ToTable("Studio");

        builder.Entity<Studio>()
        .HasMany(s => s.Movies)
        .WithOne(m => m.Studio)
        .HasForeignKey(m => m.StudioId)
        .IsRequired();
    }


}
