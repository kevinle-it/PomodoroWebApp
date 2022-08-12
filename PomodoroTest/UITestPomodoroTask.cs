using System.Linq;
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
    public class UITestPomodoroTask
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
        public void HomePageShouldDisplayAddTaskButtonInitially()
        {
            var addTaskButton = _webDriver.FindElementSafe(By.XPath("//div[text()='Add Task']"));
            addTaskButton.Exists().Should().BeTrue();
        }

        [TestMethod]
        public void ClickAddTaskButtonShouldHideAndRemoveItselfFromTheDOM()
        {
            var addTaskButton = _webDriver.FindElementSafe(
                    By.ClassName("task-manager__add-task-button")
                );
            addTaskButton.Exists().Should().BeTrue();
            addTaskButton.Click();

            addTaskButton = _webDriver.FindElementSafe(
                    By.ClassName("task-manager__add-task-button")
                );
            addTaskButton.Exists().Should().BeFalse();
        }

        [TestMethod]
        public void ClickAddTaskButtonShouldDisplayAddTaskBoxWithFormToFillIn()
        {
            var addTaskButton = _webDriver.FindElementSafe(
                    By.ClassName("task-manager__add-task-button")
                );
            addTaskButton.Exists().Should().BeTrue();
            addTaskButton.Click();

            var taskSectionWrapper = _webDriver.FindElementSafe(
                    By.ClassName("task-manager__wrapper")
                );
            taskSectionWrapper.Exists().Should().BeTrue();

            var addTaskBox = taskSectionWrapper.FindElementSafe(
                    By.TagName("form")
                );
            addTaskBox.Exists().Should().BeTrue();

            // Check task name input element
            var taskNameInput = addTaskBox.FindElementSafe(
                    By.XPath("//input[@placeholder='What are you working on?']")
                );
            taskNameInput.Exists().Should().BeTrue();
            taskNameInput.GetAttribute("placeholder").Should().Be("What are you working on?");

            // Check title of estimate pomodoro section
            var estimatePomodoroSectionTitle = addTaskBox.FindElementSafe(
                    By.ClassName("task-manager__add-task-form__estimated-poms__title")
                );
            estimatePomodoroSectionTitle.Exists().Should().BeTrue();
            estimatePomodoroSectionTitle.Text.Should().Be("Estimate Pomodoros");

            // Check initial value of estimate pomodoro input field
            var estimatePomodoroSectionInputWrapper = addTaskBox.FindElementSafe(
                    By.ClassName("task-manager__add-task-form__estimated-poms__input__wrapper")
                );
            estimatePomodoroSectionInputWrapper.Exists().Should().BeTrue();
            var estimatePomodoroSectionInput = estimatePomodoroSectionInputWrapper.FindElementSafe(
                    By.TagName("input")
                );
            estimatePomodoroSectionInput.Exists().Should().BeTrue();
            estimatePomodoroSectionInput.GetAttribute("value").Should().Be("1");
        }

        [TestMethod]
        public void ClickCancelButtonOnAddTaskBoxShouldHideAndRemoveItselfFromTheDOM()
        {
            var taskSectionWrapper = _webDriver.FindElementSafe(
                    By.ClassName("task-manager__wrapper")
                );
            taskSectionWrapper.Exists().Should().BeTrue();

            var addTaskButton = taskSectionWrapper.FindElementSafe(
                    By.ClassName("task-manager__add-task-button")
                );
            addTaskButton.Exists().Should().BeTrue();
            addTaskButton.Click();

            var submitSectionWrapper = taskSectionWrapper.FindElementSafe(
                    By.ClassName("task-manager__add-task-form__submit__wrapper")
                );
            submitSectionWrapper.Exists().Should().BeTrue();

            var cancelButton = submitSectionWrapper.FindElementSafe(
                    By.TagName("button")
                );
            cancelButton.Exists().Should().BeTrue();
            cancelButton.Click();

            // Check Add Task Box is removed
            var addTaskBox = taskSectionWrapper.FindElementSafe(
                    By.TagName("form")
                );
            addTaskBox.Exists().Should().BeFalse();

            addTaskButton = taskSectionWrapper.FindElementSafe(
                    By.ClassName("task-manager__add-task-button")
                );
            addTaskButton.Exists().Should().BeTrue();
            addTaskButton.Text.Should().Be("Add Task");
        }

        [TestMethod]
        public void ClickSaveButtonWithDataFilledInOnAddTaskBoxShouldHideAndRemoveItselfFromTheDOM()
        {
            var taskSectionWrapper = _webDriver.FindElementSafe(
                    By.ClassName("task-manager__wrapper")
                );
            taskSectionWrapper.Exists().Should().BeTrue();

            var addTaskButton = taskSectionWrapper.FindElementSafe(
                    By.ClassName("task-manager__add-task-button")
                );
            addTaskButton.Exists().Should().BeTrue();
            addTaskButton.Click();

            var addTaskBox = taskSectionWrapper.FindElementSafe(
                    By.TagName("form")
                );
            addTaskBox.Exists().Should().BeTrue();

            // Fill in Task Name data to create
            var taskNameInput = addTaskBox.FindElementSafe(
                    By.ClassName("task-manager__add-task-form__task-name-input")
                );
            taskNameInput.Exists().Should().BeTrue();
            taskNameInput.Click();
            taskNameInput.SendKeys("Test Task 1");

            var submitSectionWrapper = taskSectionWrapper.FindElementSafe(
                    By.ClassName("task-manager__add-task-form__submit__wrapper")
                );
            submitSectionWrapper.Exists().Should().BeTrue();

            var saveButton = submitSectionWrapper.FindElementSafe(
                    By.TagName("input")
                );
            saveButton.Exists().Should().BeTrue();
            saveButton.GetAttribute("value").Should().Be("Save");
            saveButton.Click();

            Thread.Sleep(2000);
            // Check Add Task Box is removed
            addTaskBox = taskSectionWrapper.FindElementSafe(
                    By.TagName("form")
                );
            addTaskBox.Exists().Should().BeFalse();

            addTaskButton = taskSectionWrapper.FindElementSafe(
                    By.ClassName("task-manager__add-task-button")
                );
            addTaskButton.Exists().Should().BeTrue();
            addTaskButton.Text.Should().Be("Add Task");
        }

        [TestMethod]
        public void ClickSaveButtonWithDataFillInShouldDisplayAddedTaskOnTaskSection()
        {
            var taskSectionWrapper = _webDriver.FindElementSafe(
                    By.ClassName("task-manager__wrapper")
                );
            taskSectionWrapper.Exists().Should().BeTrue();

            var addTaskButton = taskSectionWrapper.FindElementSafe(
                    By.ClassName("task-manager__add-task-button")
                );
            addTaskButton.Exists().Should().BeTrue();
            addTaskButton.Click();

            var addTaskBox = taskSectionWrapper.FindElementSafe(
                    By.TagName("form")
                );
            addTaskBox.Exists().Should().BeTrue();

            var taskNameInput = addTaskBox.FindElementSafe(
                    By.ClassName("task-manager__add-task-form__task-name-input")
                );
            taskNameInput.Exists().Should().BeTrue();
            taskNameInput.Click();
            taskNameInput.SendKeys("Test Task 2");

            var submitSectionWrapper = taskSectionWrapper.FindElementSafe(
                    By.ClassName("task-manager__add-task-form__submit__wrapper")
                );
            submitSectionWrapper.Exists().Should().BeTrue();

            var saveButton = submitSectionWrapper.FindElementSafe(
                    By.TagName("input")
                );
            saveButton.Exists().Should().BeTrue();
            saveButton.GetAttribute("value").Should().Be("Save");
            saveButton.Click();

            var listTasks = _webDriver.FindElementsWaitSafe(
                    By.ClassName("pomodoro-task__name"), 10
                );
            listTasks.Should().NotBeNull();
            listTasks.Count.Should().BeGreaterThan(0);
            var foundTask = listTasks.LastOrDefault(task => task.Text == "Test Task 2");
            foundTask.Should().NotBeNull();
        }

        [TestCleanup]
        public void TearDown()
        {
            _webDriver.Quit();
        }
    }
}

