using System;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace PomodoroTest
{
    [TestClass]
    public class UITestCountdownTimer
    {
        private IWebDriver _webDriver;

        [TestInitialize]
        public void SetUp()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _webDriver = new ChromeDriver();
            _webDriver.Url = "https://localhost:5001/";
            _webDriver.Navigate();
        }

        [TestMethod]
        public void CountdownTimerShouldShowDefaultPomodoroMinuteValueInitially()
        {
            var countdownTimerDigitElementList = _webDriver.FindElements(
                    By.ClassName("countdown-timer__timer__digit")
                );
            countdownTimerDigitElementList.Count.Should().Be(4);

            countdownTimerDigitElementList[0].Text.Should().Be("2");
            countdownTimerDigitElementList[1].Text.Should().Be("5");
            countdownTimerDigitElementList[2].Text.Should().Be("0");
            countdownTimerDigitElementList[3].Text.Should().Be("0");
        }

        [TestMethod]
        public void ClickStartButtonOnHomePageShouldChangeButtonTitleFromStartToStop()
        {
            var startButtonWrapper = _webDriver.FindElementSafe(
                    By.ClassName("countdown-timer__button")
                );
            startButtonWrapper.Exists().Should().BeTrue();

            var startButton = startButtonWrapper.FindElementSafe(
                    By.TagName("button")
                );
            startButton.Exists().Should().BeTrue();
            startButton.Click();

            startButton.Text.Should().Be("STOP");
        }

        [TestMethod]
        public void ClickStartButtonOnHomePageShouldTriggerCountdownTimerToRun()
        {
            var startButtonWrapper = _webDriver.FindElementSafe(
                    By.ClassName("countdown-timer__button")
                );
            startButtonWrapper.Exists().Should().BeTrue();

            var startButton = startButtonWrapper.FindElementSafe(
                    By.TagName("button")
                );
            startButton.Exists().Should().BeTrue();
            startButton.Click();

            startButton.Text.Should().Be("STOP");

            var countdownTimerDigitElementList = _webDriver.FindElements(
                    By.ClassName("countdown-timer__timer__digit")
                );
            countdownTimerDigitElementList.Count.Should().Be(4);

            Thread.Sleep(2000);
            countdownTimerDigitElementList[0].Text.Should().Be("2");
            countdownTimerDigitElementList[1].Text.Should().Be("4");
            countdownTimerDigitElementList[2].Text.Should().NotBe("0");
            countdownTimerDigitElementList[3].Text.Should().NotBe("0");
        }

        [TestCleanup]
        public void TearDown()
        {
            _webDriver.Quit();
        }
    }
}

