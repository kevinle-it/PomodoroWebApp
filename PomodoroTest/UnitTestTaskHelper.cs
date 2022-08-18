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
        [DataRow(true, 2, 2)]
        [DataRow(false, 6, 5)]
        [DataRow(false, 9, 8)]
        [DataRow(true, 15, 18)]
        [DataRow(true, 20, 20)]
        [DataRow(true, 22, 29)]
        public void taskHelperCanDetermineIfTaskIsCompleted(bool expectedIsTaskCompleted, int numEstimatedPoms, int numCompletedPoms)
        {
            Assert.AreEqual(expectedIsTaskCompleted, taskHelper.isTaskCompleted(numEstimatedPoms, numCompletedPoms));
        }
    }
}

