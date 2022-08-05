using System;
using Microsoft.EntityFrameworkCore;
using PomodoroApp.Data.Entities;

namespace PomodoroApp.Data
{
    public class PomodoroContext : DbContext
    {
        public PomodoroContext(DbContextOptions<PomodoroContext> options) : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }
    }
}

