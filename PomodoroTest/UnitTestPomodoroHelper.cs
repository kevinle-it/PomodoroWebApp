using Microsoft.VisualStudio.TestTools.UnitTesting;
using PomodoroLogic;
using static PomodoroLogic.PomodoroHelper;

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
        [DataRow(9, 8)]
        [DataRow(20, 19)]
        [DataRow(29, 28)]
        public void pomodoroHelperCanReturnNewNumCompletedLongBreaks(int expectedNewNumCompletedLongBreaks, int prevNumCompletedLongBreaks)
        {
            Assert.AreEqual(expectedNewNumCompletedLongBreaks, pomHelper.getNewNumCompletedLongBreaks(prevNumCompletedLongBreaks));
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

        [TestMethod]
        [DataRow(POMODORO_MODE.POMODORO, 0, 0, 0, 4)]
        [DataRow(POMODORO_MODE.POMODORO, 3, 2, 1, 3)]
        [DataRow(POMODORO_MODE.POMODORO, 9, 6, 3, 3)]
        [DataRow(POMODORO_MODE.POMODORO, 16, 13, 3, 5)]
        [DataRow(POMODORO_MODE.SHORT_BREAK, 8, 5, 2, 3)]
        [DataRow(POMODORO_MODE.SHORT_BREAK, 13, 8, 4, 3)]
        [DataRow(POMODORO_MODE.SHORT_BREAK, 16, 12, 3, 5)]
        [DataRow(POMODORO_MODE.SHORT_BREAK, 21, 17, 3, 6)]
        [DataRow(POMODORO_MODE.LONG_BREAK, 2, 1, 0, 2)]
        [DataRow(POMODORO_MODE.LONG_BREAK, 4, 3, 0, 4)]
        [DataRow(POMODORO_MODE.LONG_BREAK, 12, 9, 2, 4)]
        [DataRow(POMODORO_MODE.LONG_BREAK, 28, 24, 3, 7)]
        public void pomodoroHelperCanDetermineCurrentMode(
                POMODORO_MODE expectedMode,
                int numCompletedPoms,
                int numCompletedShortBreaks,
                int numCompletedLongBreaks,
                int longBreakInterval
            )
        {
            Assert.AreEqual(
                    expectedMode,
                    pomHelper.getCurrentPomodoroMode(
                            numCompletedPoms,
                            numCompletedShortBreaks,
                            numCompletedLongBreaks,
                            longBreakInterval
                        )
                );
        }
    }
}

