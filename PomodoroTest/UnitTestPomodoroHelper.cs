using Microsoft.VisualStudio.TestTools.UnitTesting;
using PomodoroLogic;

namespace PomodoroTest
{
    [TestClass]
    public class UnitTestPomodoroHelper
    {
        PomodoroHelper pomHelper;

        [TestInitialize]
        public void Instantiate()
        {
            pomHelper = new PomodoroHelper();
        }

        [TestCleanup]
        public void TearDown()
        {
            pomHelper = null;
        }

        [TestMethod]
        public void PomodoroHelperClassIsInstantiable()
        {
            Assert.IsNotNull(pomHelper);
        }

        [TestMethod]
        [DataRow(2, 1)]
        [DataRow(6, 5)]
        [DataRow(9, 8)]
        public void pomodoroHelperCanReturnNewNumCompletedPoms(int expectedNewNumCompletedPoms, int prevNumCompletedPoms)
        {
            Assert.AreEqual(expectedNewNumCompletedPoms, pomHelper.getNewNumCompletedPoms(prevNumCompletedPoms));
        }

        [TestMethod]
        [DataRow(3, 2)]
        [DataRow(8, 7)]
        [DataRow(15, 14)]
        public void pomodoroHelperCanReturnNewNumCompletedShortBreaks(int expectedNewNumCompletedShortBreaks, int prevNumCompletedShortBreaks)
        {
            Assert.AreEqual(expectedNewNumCompletedShortBreaks, pomHelper.getNewNumCompletedShortBreaks(prevNumCompletedShortBreaks));
        }

        [TestMethod]
        [DataRow(true, 3, 2)]
        [DataRow(false, 8, 6)]
        [DataRow(false, 25, 20)]
        [DataRow(true, 29, 28)]
        public void pomodoroHelperCanDetermineIfLongBreakShouldBeStarted(bool expectedShouldStartLongBreak, int longBreakInterval, int numCompletedShortBreaks)
        {
            Assert.AreEqual(expectedShouldStartLongBreak, pomHelper.shouldStartLongBreak(longBreakInterval, numCompletedShortBreaks));
        }
    }
}

