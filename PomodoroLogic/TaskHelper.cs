using System;
namespace PomodoroLogic
{
    public class TaskHelper
    {
        public bool isTaskCompleted(int numEstimatedPoms, int numCompletedPoms, bool isCompletedLongBreak)
        {
            return numCompletedPoms >= numEstimatedPoms && isCompletedLongBreak;
        }
    }
}

