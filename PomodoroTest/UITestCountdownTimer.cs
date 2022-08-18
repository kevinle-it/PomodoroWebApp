using System;
using System.Collections.Generic;
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

        public IWebElement ClickSettingsMenuShouldNavigateToSettingsPage()
        {
            var settingsMenu = _webDriver.FindElementSafe(
                    By.CssSelector(".nav-item a[href='/settings']")
                );
            settingsMenu.Exists().Should().BeTrue();
            settingsMenu.Click();

            var settingsContainer = _webDriver.FindElementSafe(
                    By.CssSelector("form.settings__wrapper")
                );
            settingsContainer.Exists().Should().BeTrue();

            var settingsTitle = settingsContainer.FindElementSafe(
                    By.ClassName("settings__title")
                );
            settingsTitle.Exists().Should().BeTrue();
            settingsTitle.Text.Should().Be("Timer Settings");

            return settingsContainer;
        }

        [TestMethod]
        [TestCategory("Initialization")]
        public void CountdownTimerShouldShowCorrectPomodoroMinuteValueFromConfigsInitially()
        {
            Thread.Sleep(3000);
            var settingsContainer = ClickSettingsMenuShouldNavigateToSettingsPage();

            var sectionTimeInputs = settingsContainer.FindElements(
                    By.CssSelector(".settings__section__time__wrapper > input[name='time-pomodoro']")
                );
            int timeValue = 1;
            int.TryParse(sectionTimeInputs[0].GetAttribute("value"), out timeValue);

            _webDriver.Navigate().Back();

            var digitList = getDigitList(timeValue);
            if (timeValue < 100)
            {
                var countdownTimerDigitElementList = _webDriver.FindElements(
                    By.ClassName("countdown-timer__timer__digit")
                );
                countdownTimerDigitElementList.Count.Should().Be(4);

                countdownTimerDigitElementList[0].Text.Should().Be(digitList[0].ToString());
                countdownTimerDigitElementList[1].Text.Should().Be(digitList[1].ToString());
                countdownTimerDigitElementList[2].Text.Should().Be("0");
                countdownTimerDigitElementList[3].Text.Should().Be("0");
            }
            else
            {
                var timerElementList = _webDriver.FindElements(
                    By.CssSelector(".countdown-timer__timer > div")
                );
                var leftDigitElements = timerElementList[0].FindElements(By.TagName("div"));
                for (int i = 0; i < leftDigitElements.Count; i++)
                {
                    leftDigitElements[i].Text.Should().Be(digitList[i].ToString());
                }
                timerElementList[1].Text.Should().Be(digitList[digitList.Count-1].ToString());
                timerElementList[2].Text.Should().Be(":");
                timerElementList[3].Text.Should().Be("0");
                timerElementList[4].Text.Should().Be("0");
            }
        }

        [TestMethod]
        [TestCategory("Timer")]
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
            Thread.Sleep(3000);
            var settingsContainer = ClickSettingsMenuShouldNavigateToSettingsPage();

            var sectionTimeInputs = settingsContainer.FindElements(
                    By.CssSelector(".settings__section__time__wrapper > input[name='time-pomodoro']")
                );
            int timeValue = 1;
            int.TryParse(sectionTimeInputs[0].GetAttribute("value"), out timeValue);

            _webDriver.Navigate().Back();

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
            Thread.Sleep(2000);

            var digitList = getDigitList(timeValue - 1);
            if (timeValue < 100)
            {
                var countdownTimerDigitElementList = _webDriver.FindElements(
                    By.ClassName("countdown-timer__timer__digit")
                );
                countdownTimerDigitElementList.Count.Should().Be(4);

                countdownTimerDigitElementList[0].Text.Should().Be(digitList[0].ToString());
                countdownTimerDigitElementList[1].Text.Should().Be(digitList[1].ToString());
                countdownTimerDigitElementList[2].Text.Should().NotBe("0");
                countdownTimerDigitElementList[3].Text.Should().NotBe("0");
            }
            else
            {
                var timerElementList = _webDriver.FindElements(
                    By.CssSelector(".countdown-timer__timer > div")
                );
                var leftDigitElements = timerElementList[0].FindElements(By.TagName("div"));
                for (int i = 0; i < leftDigitElements.Count; i++)
                {
                    leftDigitElements[i].Text.Should().Be(digitList[i].ToString());
                }
                timerElementList[1].Text.Should().Be(digitList[digitList.Count - 1].ToString());
                timerElementList[2].Text.Should().Be(":");
                timerElementList[3].Text.Should().NotBe("0");
                timerElementList[4].Text.Should().NotBe("0");
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            _webDriver.Quit();
        }

        private List<int> getDigitList(int value)
        {
            var numDigits = 0;
            var digitsList = new List<int>();
            do
            {
                digitsList.Insert(0, value % 10);
                value /= 10;
                ++numDigits;
            } while (value > 0);

            return digitsList;
        }
    }
}

