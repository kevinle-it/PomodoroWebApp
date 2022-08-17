using System;

namespace PomodoroLogic
{
    public class PomodoroHelper
    {
        public enum POMODORO_MODE
        {
            POMODORO,
            SHORT_BREAK,
            LONG_BREAK
        }

        public int getNewNumCompletedPoms(int prevNumCompletedPoms)
        {
            return prevNumCompletedPoms + 1;
        }

        public int getNewNumCompletedShortBreaks(int prevNumCompletedShortBreaks)
        {
            return prevNumCompletedShortBreaks + 1;
        }

        public bool shouldStartLongBreak(int longBreakInterval, int numCompletedShortBreaks)
        {
            return numCompletedShortBreaks + 1 == longBreakInterval;
        }

        public POMODORO_MODE getCurrentPomodoroMode(
            int numCompletedPoms,
            int numCompletedShortBreaks,
            int longBreakInterval)
        {
            if (numCompletedPoms == 0)
            {
                return POMODORO_MODE.POMODORO;
            }
            if (numCompletedPoms % longBreakInterval != 0)
            {
                if (numCompletedShortBreaks < numCompletedPoms)
                {
                    return POMODORO_MODE.SHORT_BREAK;
                }
                return POMODORO_MODE.POMODORO;
            }
            return POMODORO_MODE.LONG_BREAK;
        }
    }
}

