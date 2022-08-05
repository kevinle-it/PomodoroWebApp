using System;

namespace PomodoroLogic
{
    public class PomodoroHelper
    {
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
    }
}

