using System;
using Microsoft.EntityFrameworkCore;

namespace PomodoroApp.Data
{
    public class PomodoroContext : DbContext
    {
        public PomodoroContext(DbContextOptions<PomodoroContext> options) : base(options)
        {
        }
    }
}

