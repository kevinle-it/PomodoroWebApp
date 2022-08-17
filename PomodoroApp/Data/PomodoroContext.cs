using System;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using PomodoroApp.Data.Entities;

namespace PomodoroApp.Data
{
    public class PomodoroContext : DbContext
    {
        public PomodoroContext(DbContextOptions<PomodoroContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PomodoroConfiguration>()
                .HasData(
                    new PomodoroConfiguration
                    {
                        Id = 1,
                        PomodoroLength = 25,
                        ShortBreakLength = 5,
                        LongBreakLength = 15,
                        AutoStartPom = false,
                        AutoStartBreak = false,
                        LongBreakInterval = 4,
                    }
                );

            builder.Entity<PomodoroConfiguration>()
                .Property(pc => pc.PomodoroLength)
                .HasDefaultValue(25);

            builder.Entity<PomodoroConfiguration>()
                .Property(pc => pc.ShortBreakLength)
                .HasDefaultValue(5);

            builder.Entity<PomodoroConfiguration>()
                .Property(pc => pc.LongBreakLength)
                .HasDefaultValue(15);

            builder.Entity<PomodoroConfiguration>()
                .Property(pc => pc.LongBreakInterval)
                .HasDefaultValue(4);
        }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<PomodoroConfiguration> PomodoroConfigurations { get; set; }
    }
}

