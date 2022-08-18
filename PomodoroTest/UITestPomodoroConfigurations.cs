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
    public class UITestPomodoroConfigurations
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
        [TestCategory("Initilization")]
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
        [TestCategory("Initilization")]
        public void ClickSettingsMenuShouldDisplaySettingsPage()
        {
            var settingsContainer = ClickSettingsMenuShouldNavigateToSettingsPage();

            var sectionTitles = settingsContainer.FindElements(
                    By.ClassName("settings__section__title")
                );
            sectionTitles.Count.Should().Be(3);
            sectionTitles[0].Text.Should().Be("Time (minutes)");
            sectionTitles[1].Text.Should().Be("Auto start");
            sectionTitles[2].Text.Should().Be("Long Break interval");

            var sectionTimeSubtitles = settingsContainer.FindElements(
                    By.CssSelector(".settings__section__time__wrapper > div")
                );
            sectionTimeSubtitles[0].Text.Should().Be("Pomodoro");
            sectionTimeSubtitles[1].Text.Should().Be("Short Break");
            sectionTimeSubtitles[2].Text.Should().Be("Long Break");

            var sectionAutoSubtitles = settingsContainer.FindElements(
                    By.CssSelector(".settings__section__auto__wrapper > div")
                );
            sectionAutoSubtitles[0].Text.Should().Be("Pomodoros");
            sectionAutoSubtitles[1].Text.Should().Be("Breaks");

            var saveButton = settingsContainer.FindElementSafe(
                    By.CssSelector(".settings__section__save__wrapper > input")
                );
            saveButton.Exists().Should().BeTrue();
            saveButton.GetAttribute("value").Should().Be("Save");
        }

        [TestMethod]
        [TestCategory("Initilization")]
        public void SaveButtonShouldBeDisabledOnSettingsPageDisplayInitially()
        {
            var settingsContainer = ClickSettingsMenuShouldNavigateToSettingsPage();

            var saveButton = settingsContainer.FindElementSafe(
                    By.CssSelector(".settings__section__save__wrapper > input")
                );
            saveButton.Exists().Should().BeTrue();
            saveButton.Enabled.Should().BeFalse();
        }

        [TestMethod]
        [TestCategory("UpdateConfigs")]
        [DataRow("time-pomodoro", 1, false, false)]
        [DataRow("time-short-break", 1, false, false)]
        [DataRow("time-long-break", 1, false, false)]
        [DataRow("auto-start-pomodoros", 1, false, false)]
        [DataRow("auto-start-breaks", 1, false, false)]
        [DataRow("long-break-interval", 1, false, false)]
        public void SaveButtonShouldBeEnabledOnAnyConfigValuesChange(
                string fieldToChange,
                int valueChangeBy,
                bool shouldOverwriteValue,
                bool isReused
            )
        {
            var settingsContainer = ClickSettingsMenuShouldNavigateToSettingsPage();

            var saveButton = settingsContainer.FindElementSafe(
                    By.CssSelector(".settings__section__save__wrapper > input")
                );
            saveButton.Exists().Should().BeTrue();
            if (!isReused)
            {
                saveButton.Enabled.Should().BeFalse();
            }

            var sectionTimeInputs = settingsContainer.FindElements(
                    By.CssSelector(".settings__section__time__wrapper > input")
                );
            sectionTimeInputs[0].GetAttribute("name").Should().Be("time-pomodoro");
            sectionTimeInputs[1].GetAttribute("name").Should().Be("time-short-break");
            sectionTimeInputs[2].GetAttribute("name").Should().Be("time-long-break");

            var sectionAutoLabels = settingsContainer.FindElements(
                    By.CssSelector(".settings__section__auto__wrapper > label")
                );
            var sectionAutoInputs = settingsContainer.FindElements(
                    By.CssSelector(".settings__section__auto__wrapper input")
                );
            sectionAutoInputs[0].GetAttribute("name").Should().Be("auto-start-pomodoros");
            sectionAutoInputs[1].GetAttribute("name").Should().Be("auto-start-breaks");

            var sectionIntervalInput = settingsContainer.FindElementSafe(
                    By.CssSelector(".settings__section__interval__wrapper > input")
                );
            sectionIntervalInput.Exists().Should().BeTrue();

            switch (fieldToChange)
            {
                case "time-pomodoro":
                    int timeValue = 1;
                    int.TryParse(sectionTimeInputs[0].GetAttribute("value"), out timeValue);
                    sectionTimeInputs[0].Clear();
                    if (shouldOverwriteValue)
                    {
                        sectionTimeInputs[0].SendKeys(valueChangeBy.ToString());
                    }
                    else
                    {
                        sectionTimeInputs[0].SendKeys((timeValue + valueChangeBy).ToString());
                    }
                    break;
                case "time-short-break":
                    timeValue = 1;
                    int.TryParse(sectionTimeInputs[1].GetAttribute("value"), out timeValue);
                    sectionTimeInputs[1].Clear();
                    if (shouldOverwriteValue)
                    {
                        sectionTimeInputs[1].SendKeys(valueChangeBy.ToString());
                    }
                    else
                    {
                        sectionTimeInputs[1].SendKeys((timeValue + valueChangeBy).ToString());
                    }
                    break;
                case "time-long-break":
                    timeValue = 1;
                    int.TryParse(sectionTimeInputs[2].GetAttribute("value"), out timeValue);
                    sectionTimeInputs[2].Clear();
                    if (shouldOverwriteValue)
                    {
                        sectionTimeInputs[2].SendKeys(valueChangeBy.ToString());
                    }
                    else
                    {
                        sectionTimeInputs[2].SendKeys((timeValue + valueChangeBy).ToString());
                    }
                    break;
                case "auto-start-pomodoros":
                    sectionAutoLabels[0].Click();
                    break;
                case "auto-start-breaks":
                    sectionAutoLabels[1].Click();
                    break;
                case "long-break-interval":
                    timeValue = 1;
                    int.TryParse(sectionIntervalInput.GetAttribute("value"), out timeValue);
                    sectionIntervalInput.Clear();
                    if (shouldOverwriteValue)
                    {
                        sectionIntervalInput.SendKeys(valueChangeBy.ToString());
                    }
                    else
                    {
                        sectionIntervalInput.SendKeys((timeValue + valueChangeBy).ToString());
                    }
                    break;
                default:
                    sectionAutoLabels[0].Click();
                    break;
            }
            if (!isReused)
            {
                saveButton.Enabled.Should().BeTrue();
            }
        }

        [TestMethod]
        [TestCategory("UpdateConfigs")]
        [DataRow("time-pomodoro", 1)]
        [DataRow("time-short-break", 1)]
        [DataRow("time-long-break", 1)]
        [DataRow("auto-start-pomodoros", 1)]
        [DataRow("auto-start-breaks", 1)]
        [DataRow("long-break-interval", 1)]
        public void SaveButtonShouldBeDisabledIfConfigValuesAreSameAsLastUpdatedValuesAfterAll(
                string fieldToChange,
                int valueChangeBy
            )
        {
            var settingsContainer = ClickSettingsMenuShouldNavigateToSettingsPage();
            var saveButton = settingsContainer.FindElementSafe(
                    By.CssSelector(".settings__section__save__wrapper > input")
                );
            saveButton.Exists().Should().BeTrue();
            saveButton.Enabled.Should().BeFalse();

            SaveButtonShouldBeEnabledOnAnyConfigValuesChange(
                    fieldToChange,
                    valueChangeBy,
                    shouldOverwriteValue: false,
                    isReused: true
                );
            Thread.Sleep(2000);

            saveButton.Enabled.Should().BeTrue();

            SaveButtonShouldBeEnabledOnAnyConfigValuesChange(
                    fieldToChange,
                    -valueChangeBy,
                    shouldOverwriteValue: false,
                    isReused: true
                );
            Thread.Sleep(2000);

            saveButton.Enabled.Should().BeFalse();
        }

        [TestMethod]
        [TestCategory("UpdateConfigs")]
        [DataRow("time-pomodoro", 1)]
        public void ClickSaveButtonShouldUpdateConfigValues(string fieldToChange, int valueChangeBy)
        {
            var settingsContainer = ClickSettingsMenuShouldNavigateToSettingsPage();
            var saveButton = settingsContainer.FindElementSafe(
                    By.CssSelector(".settings__section__save__wrapper > input")
                );
            saveButton.Exists().Should().BeTrue();

            SaveButtonShouldBeEnabledOnAnyConfigValuesChange(
                    fieldToChange,
                    valueChangeBy,
                    shouldOverwriteValue: false,    // only increase current time by 1 and save, min == 1 => newTime >= 2
                    isReused: true
                );
            saveButton.Enabled.Should().BeTrue();
            saveButton.Click();

            Thread.Sleep(3000);

            SaveButtonShouldBeEnabledOnAnyConfigValuesChange(
                    fieldToChange,
                    valueChangeBy,
                    shouldOverwriteValue: true,     // overwrite current time (>= 2) by value 1
                    isReused: true
                );
            saveButton.Enabled.Should().BeTrue();
            saveButton.Click();

            Thread.Sleep(3000);

            _webDriver.Navigate().Back();

            var countdownTimerDigitElementList = _webDriver.FindElements(
                    By.ClassName("countdown-timer__timer__digit")
                );
            countdownTimerDigitElementList.Count.Should().Be(4);

            countdownTimerDigitElementList[0].Text.Should().Be("0");
            countdownTimerDigitElementList[1].Text.Should().Be("1");
            countdownTimerDigitElementList[2].Text.Should().Be("0");
            countdownTimerDigitElementList[3].Text.Should().Be("0");
        }

        [TestCleanup]
        public void TearDown()
        {
            _webDriver.Quit();
        }
    }
}

