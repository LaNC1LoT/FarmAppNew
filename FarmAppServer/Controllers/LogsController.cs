using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using FarmAppServer.Services.Paging;

namespace FarmAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly FarmAppContext _context;

        public LogsController(FarmAppContext context)
        {
            _context = context;
        }

        // GET: api/Logs
        [HttpGet]
        public ActionResult<PagedResult<Log>> GetLogs([FromQuery]int page = 1, [FromQuery]int pageSize = 25)
        {
            try
            {
                IQueryable<Log> logs = _context.Logs;
                var query = logs.GetPaged(page, pageSize);

                return Ok(query);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseBody()
                {
                    Header = "Error",
                    Result = $"{e.Message}"
                });
            }
        }

        // GET: api/Logs/5
        [HttpGet("LogById")]
        public async Task<ActionResult<Log>> GetLog([FromQuery]int id)
        {
            var log = await _context.Logs.FindAsync(id);

            if (log == null)
            {
                return NotFound();
            }

            return log;
        }

        // PUT: api/Logs/5
        [HttpPut("{id}")]
        [NonAction]
        public async Task<IActionResult> PutLog(int id, Log log)
        {
            if (id != log.Id)
            {
                return BadRequest();
            }

            _context.Entry(log).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogExists(id))
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

        // POST: api/Logs
        [HttpPost]
        [NonAction]
        public async Task<ActionResult<Log>> PostLog(Log log)
        {
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLog", new { id = log.Id }, log);
        }

        // DELETE: api/Logs/5
        [HttpDelete("{id}")]
        [NonAction]
        public async Task<ActionResult<Log>> DeleteLog(int id)
        {
            var log = await _context.Logs.FindAsync(id);
            if (log == null)
            {
                return NotFound();
            }

            _context.Logs.Remove(log);
            await _context.SaveChangesAsync();

            return log;
        }

        private bool LogExists(int id)
        {
            return _context.Logs.Any(e => e.Id == id);
        }

        [HttpGet("LogForPeriod")]
        public IActionResult LogForPeriod([FromQuery]DateTime start, [FromQuery]DateTime end, [FromQuery]int page = 1, [FromQuery]int pageSize = 25)
        {
            var logs = _context.Logs.Where(x => x.RequestTime >= start && x.RequestTime <= end);
            var result = logs.GetPaged(page, pageSize);

            if (result == null)
                return NotFound(new ResponseBody()
                {
                    Header = "Error",
                    Result = "Logs not found"
                });

            return Ok(result);
        }
    }
}
