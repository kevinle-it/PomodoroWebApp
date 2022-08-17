using System;
using System.ComponentModel.DataAnnotations;

namespace PomodoroApp.Data.Entities
{
    public class PomodoroConfiguration
    {
        [Key]
        [Required]
        public int Id { get; set; }

        private int _pomodoroLength = 25;
        [Required]
        public int PomodoroLength
        {
            get => _pomodoroLength;
            set
            {
                if (value > 0)
                {
                    _pomodoroLength = value;
                }
            }
        }

        private int _shortBreakLength = 5;
        [Required]
        public int ShortBreakLength
        {
            get => _shortBreakLength;
            set
            {
                if (value > 0)
                {
                    _shortBreakLength = value;
                }
            }
        }

        private int _longBreakLength = 15;
        [Required]
        public int LongBreakLength
        {
            get => _longBreakLength;
            set
            {
                if (value > 0)
                {
                    _longBreakLength = value;
                }
            }
        }

        [Required]
        public bool AutoStartPom { get; set; }

        [Required]
        public bool AutoStartBreak { get; set; }

        private int _longBreakInterval = 4;
        [Required]
        public int LongBreakInterval
        {
            get => _longBreakInterval;
            set
            {
                if (value > 0)
                {
                    _longBreakInterval = value;
                }
            }
        }
    }
}

