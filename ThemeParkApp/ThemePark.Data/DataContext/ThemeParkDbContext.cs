﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ThemePark.Data.DataContext.Configuration;
using ThemePark.Data.DataContext.Models;

namespace ThemePark.Data.DataContext;

public class ThemeParkDbContext : DbContext, IThemeParkDbContext
{
    private readonly IConfiguration _configuration;

    public virtual DbSet<Attraction> Attractions { get; set; } = null!;
    public virtual DbSet<Location> Locations { get; set; } = null!;

    public ThemeParkDbContext(
        DbContextOptions<ThemeParkDbContext> options,
        IConfiguration configuration
    )
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
            return;

        var connString = _configuration.GetConnectionString("DefaultConnection");
        if (!string.IsNullOrEmpty(connString))
        {
            optionsBuilder.UseSqlServer(connString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AttractionConfiguration());
        modelBuilder.ApplyConfiguration(new LocationConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
