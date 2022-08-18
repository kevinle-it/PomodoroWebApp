using System;
using System.ComponentModel.DataAnnotations;

namespace PomodoroApp.Data.Entities
{
    public class Task
    {
        [Key]
        [Required]
        public int TaskId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int NumEstimatedPoms { get; set; }

        [Required]
        public int NumCompletedPoms { get; set; }

        [Required]
        public int NumCompletedShortBreaks { get; set; }

        [Required]
        public int NumCompletedLongBreaks { get; set; }

        [Required]
        public DateTime DateTimeCreated{ get; set; }
    }
}

