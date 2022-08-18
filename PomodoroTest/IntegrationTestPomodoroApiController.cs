using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PomodoroApp.Controllers;
using PomodoroApp.Data;
using PomodoroTask = PomodoroApp.Data.Entities.Task;

namespace PomodoroTest
{
    [TestClass]
    public class IntegrationTestPomodoroApiController
    {
        [TestMethod]
        public void PomodoroApiControllerCanReturnAllTasks()
        {
            DbContextOptionsBuilder<PomodoroContext> optionsBuilder = new();
            optionsBuilder.UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name);

            var dateTimeCreatedTask = new DateTime(2022, 8, 4, 22, 59, 59, DateTimeKind.Utc);
            using (PomodoroContext ctx = new(optionsBuilder.Options))
            {
                ctx.Add(new PomodoroTask {
                    Name = "Complete Web Final Project",
                    NumEstimatedPoms = 4,
                    NumCompletedPoms = 4,
                    NumCompletedShortBreaks = 3,
                    NumCompletedLongBreaks = 1,
                    DateTimeCreated = dateTimeCreatedTask
                });
                ctx.SaveChanges();
            }

            Task<ActionResult<IEnumerable<PomodoroTask>>> result;
            using (PomodoroContext ctx = new(optionsBuilder.Options))
            {
                result = new PomodoroController(ctx).GetPomodoroTasks();
            }

            List<PomodoroTask> pomodoroTasks = (List<PomodoroTask>) result.Result.Value;
            Assert.IsNotNull(pomodoroTasks);
            Assert.AreEqual(1, pomodoroTasks.Count);

            var firstPomodoroTask = pomodoroTasks.First();
            Assert.AreEqual(1, firstPomodoroTask.TaskId);
            Assert.AreEqual("Complete Web Final Project", firstPomodoroTask.Name);
            Assert.AreEqual(4, firstPomodoroTask.NumEstimatedPoms);
            Assert.AreEqual(4, firstPomodoroTask.NumCompletedPoms);
            Assert.AreEqual(3, firstPomodoroTask.NumCompletedShortBreaks);
            Assert.AreEqual(1, firstPomodoroTask.NumCompletedLongBreaks);
            Assert.AreEqual(dateTimeCreatedTask, firstPomodoroTask.DateTimeCreated);
        }

        [TestMethod]
        public void PomodoroApiControllerCanProcessOnCurrentPomodoroComplete()
        {
            DbContextOptionsBuilder<PomodoroContext> optionsBuilder = new();
            optionsBuilder.UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name);

            var dateTimeCreatedTask = new DateTime(2022, 8, 4, 22, 59, 59, DateTimeKind.Utc);
            using (PomodoroContext ctx = new(optionsBuilder.Options))
            {
                ctx.Add(new PomodoroTask
                {
                    Name = "Complete Web Final Project",
                    NumEstimatedPoms = 4,
                    NumCompletedPoms = 3,
                    NumCompletedShortBreaks = 3,
                    NumCompletedLongBreaks = 0,
                    DateTimeCreated = dateTimeCreatedTask
                });
                ctx.SaveChanges();
            }

            Task<ActionResult<PomodoroTask>> result;
            using (PomodoroContext ctx = new(optionsBuilder.Options))
            {
                result = new PomodoroController(ctx).OnCurrentPomodoroComplete(1);
            }

            PomodoroTask pomodoroTask = (PomodoroTask) ((OkObjectResult) result.Result.Result).Value;
            Assert.IsNotNull(pomodoroTask);

            Assert.AreEqual(1, pomodoroTask.TaskId);
            Assert.AreEqual("Complete Web Final Project", pomodoroTask.Name);
            Assert.AreEqual(4, pomodoroTask.NumEstimatedPoms);
            Assert.AreEqual(4, pomodoroTask.NumCompletedPoms);
            Assert.AreEqual(3, pomodoroTask.NumCompletedShortBreaks);
            Assert.AreEqual(0, pomodoroTask.NumCompletedLongBreaks);
            Assert.AreEqual(dateTimeCreatedTask, pomodoroTask.DateTimeCreated);

            using (PomodoroContext ctx = new(optionsBuilder.Options))
            {
                PomodoroTask inDbPomodoroTask = ctx.Tasks.FindAsync(1).Result;

                Assert.AreEqual(1, inDbPomodoroTask.TaskId);
                Assert.AreEqual("Complete Web Final Project", inDbPomodoroTask.Name);
                Assert.AreEqual(4, inDbPomodoroTask.NumEstimatedPoms);
                Assert.AreEqual(4, inDbPomodoroTask.NumCompletedPoms);
                Assert.AreEqual(3, inDbPomodoroTask.NumCompletedShortBreaks);
                Assert.AreEqual(0, inDbPomodoroTask.NumCompletedLongBreaks);
                Assert.AreEqual(dateTimeCreatedTask, inDbPomodoroTask.DateTimeCreated);
            }
        }
    }
}

