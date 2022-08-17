using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PomodoroApp.Data;
using PomodoroApp.Data.Entities;
using PomodoroLogic;
using PomodoroTask = PomodoroApp.Data.Entities.Task;

namespace PomodoroApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PomodoroController : ControllerBase
    {
        private readonly PomodoroContext _context;
        private PomodoroHelper pomodoroHelper;

        public PomodoroController(PomodoroContext context)
        {
            _context = context;
            pomodoroHelper = new PomodoroHelper();
        }

        // GET: api/Pomodoro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PomodoroTask>>> GetPomodoroTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // PUT: api/Pomodoro/complete/pomodoro/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("complete/pomodoro/{taskId}")]
        public async Task<ActionResult<PomodoroTask>> OnCurrentPomodoroComplete(int taskId)
        {
            var pomodoroTask = await _context.Tasks.FindAsync(taskId);

            if (pomodoroTask == null)
            {
                return NotFound("Task not found.");
            }

            var pomodoroConfig = await _context.PomodoroConfigurations.FirstOrDefaultAsync();
            // Validate current Pomodoro Mode
            var pomodoroMode = pomodoroHelper.getCurrentPomodoroMode(
                    pomodoroTask.NumCompletedPoms,
                    pomodoroTask.NumCompletedShortBreaks,
                    pomodoroConfig.LongBreakInterval
                );
            if (pomodoroMode != PomodoroHelper.POMODORO_MODE.POMODORO)
            {
                return BadRequest($"Invalid Mode [Pomodoro] request. Request this mode instead: {pomodoroMode.ToString()}");
            }

            // Update number of completed Pomodoros
            pomodoroTask.NumCompletedPoms = pomodoroHelper.getNewNumCompletedPoms(pomodoroTask.NumCompletedPoms);
            _context.Entry(pomodoroTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok(pomodoroTask);
        }

        // PUT: api/Pomodoro/complete/shortbreak/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("complete/shortbreak/{taskId}")]
        public async Task<ActionResult<PomodoroTask>> OnCurrentShortBreakComplete(int taskId)
        {
            var pomodoroTask = await _context.Tasks.FindAsync(taskId);

            if (pomodoroTask == null)
            {
                return NotFound("Task not found.");
            }

            var pomodoroConfig = await _context.PomodoroConfigurations.FirstOrDefaultAsync();
            // Validate current Pomodoro Mode
            var pomodoroMode = pomodoroHelper.getCurrentPomodoroMode(
                    pomodoroTask.NumCompletedPoms,
                    pomodoroTask.NumCompletedShortBreaks,
                    pomodoroConfig.LongBreakInterval
                );
            if (pomodoroMode != PomodoroHelper.POMODORO_MODE.SHORT_BREAK)
            {
                return BadRequest($"Invalid Mode [Short Break] request. Request this mode instead: {pomodoroMode.ToString()}");
            }

            // Update number of completed Short Breaks
            pomodoroTask.NumCompletedShortBreaks = pomodoroHelper.getNewNumCompletedShortBreaks(pomodoroTask.NumCompletedShortBreaks);
            _context.Entry(pomodoroTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok(pomodoroTask);
        }

        // POST: api/Pomodoro
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PomodoroTask>> PostPomodoroTask([FromBody] PomodoroTask pomodoroTask)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(pomodoroTask.Name))
                {
                    return BadRequest("Pomodoro Task Name should not be null or empty.");
                }
                pomodoroTask.NumCompletedPoms = 0;
                pomodoroTask.NumCompletedShortBreaks = 0;
                pomodoroTask.IsCompletedLongBreak = false;
                pomodoroTask.DateTimeCreated = DateTime.UtcNow;
                _context.Tasks.Add(pomodoroTask);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTask", new { id = pomodoroTask.TaskId }, pomodoroTask);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("config")]
        public async Task<ActionResult<PomodoroConfiguration>> UpdatePomodoroConfigs([FromBody] PomodoroConfiguration newPomodoroConfigs)
        {
            if (ModelState.IsValid)
            {
                var pomodoroConfigs = await _context.PomodoroConfigurations.FirstOrDefaultAsync();

                if (newPomodoroConfigs.PomodoroLength != pomodoroConfigs.PomodoroLength
                    || newPomodoroConfigs.ShortBreakLength != pomodoroConfigs.ShortBreakLength
                    || newPomodoroConfigs.LongBreakLength != pomodoroConfigs.LongBreakLength
                    || newPomodoroConfigs.AutoStartPom != pomodoroConfigs.AutoStartPom
                    || newPomodoroConfigs.AutoStartBreak != pomodoroConfigs.AutoStartBreak
                    || newPomodoroConfigs.LongBreakInterval != pomodoroConfigs.LongBreakInterval)
                {
                    pomodoroConfigs.PomodoroLength = newPomodoroConfigs.PomodoroLength;
                    pomodoroConfigs.ShortBreakLength = newPomodoroConfigs.ShortBreakLength;
                    pomodoroConfigs.LongBreakLength = newPomodoroConfigs.LongBreakLength;
                    pomodoroConfigs.AutoStartPom = newPomodoroConfigs.AutoStartPom;
                    pomodoroConfigs.AutoStartBreak = newPomodoroConfigs.AutoStartBreak;
                    pomodoroConfigs.LongBreakInterval = newPomodoroConfigs.LongBreakInterval;
                    _context.Entry(pomodoroConfigs).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                return Ok(pomodoroConfigs);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("config")]
        public async Task<ActionResult<PomodoroConfiguration>> GetPomodoroConfigs()
        {
            if (ModelState.IsValid)
            {
                var pomodoroConfigs = await _context.PomodoroConfigurations.FirstOrDefaultAsync();

                return Ok(pomodoroConfigs);
            }
            return BadRequest(ModelState);
        }

        private bool PomodoroTaskExists(int id)
        {
            return _context.Tasks.Any(e => e.TaskId == id);
        }
    }
}
