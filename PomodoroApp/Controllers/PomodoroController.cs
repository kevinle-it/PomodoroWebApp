using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PomodoroApp.Data;
using PomodoroTask = PomodoroApp.Data.Entities.Task;

namespace PomodoroApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PomodoroController : ControllerBase
    {
        private readonly PomodoroContext _context;

        public PomodoroController(PomodoroContext context)
        {
            _context = context;
        }

        // GET: api/Pomodoro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PomodoroTask>>> GetPomodoroTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/Pomodoro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PomodoroTask>> GetPomodoroTask(int id)
        {
            var pomodoroTask = await _context.Tasks.FindAsync(id);

            if (pomodoroTask == null)
            {
                return NotFound();
            }

            return pomodoroTask;
        }

        // PUT: api/Pomodoro/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPomodoroTask(int id, PomodoroTask pomodoroTask)
        {
            if (id != pomodoroTask.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(pomodoroTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PomodoroTaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pomodoro
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PomodoroTask>> PostPomodoroTask(PomodoroTask pomodoroTask)
        {
            _context.Tasks.Add(pomodoroTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = pomodoroTask.TaskId }, pomodoroTask);
        }

        // DELETE: api/Pomodoro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePomodoroTask(int id)
        {
            var pomodoroTask = await _context.Tasks.FindAsync(id);
            if (pomodoroTask == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(pomodoroTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PomodoroTaskExists(int id)
        {
            return _context.Tasks.Any(e => e.TaskId == id);
        }
    }
}