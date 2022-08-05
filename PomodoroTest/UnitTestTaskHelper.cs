using Microsoft.VisualStudio.TestTools.UnitTesting;
using PomodoroLogic;

namespace PomodoroTest
{
    [TestClass]
    public class UnitTestTaskHelper
    {
        TaskHelper taskHelper;

        [TestInitialize]
        public void Instantiate()
        {
            taskHelper = new TaskHelper();
        }

        [TestCleanup]
        public void TearDown()
        {
            taskHelper = null;
        }

        [TestMethod]
        public void TaskHelperClassIsInstantiable()
        {
            Assert.IsNotNull(taskHelper);
        }

        [TestMethod]
        [DataRow(true, 2, 2, true)]
        [DataRow(false, 6, 5, false)]
        [DataRow(false, 9, 8, false)]
        [DataRow(true, 15, 18, true)]
        [DataRow(true, 20, 20, true)]
        [DataRow(true, 22, 29, true)]
        public void taskHelperCanDetermineIfTaskIsCompleted(bool expectedIsTaskCompleted, int numEstimatedPoms, int numCompletedPoms, bool isCompletedLongBreak)
        {
            Assert.AreEqual(expectedIsTaskCompleted, taskHelper.isTaskCompleted(numEstimatedPoms, numCompletedPoms, isCompletedLongBreak));
        }
    }
}

