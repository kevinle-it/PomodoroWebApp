using System;
namespace PomodoroLogic
{
    public class TaskHelper
    {
        public bool isTaskCompleted(int numEstimatedPoms, int numCompletedPoms)
        {
            return numCompletedPoms >= numEstimatedPoms;
        }
    }
}

