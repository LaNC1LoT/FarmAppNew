using AutoMapper;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FarmAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly FarmAppContext _farmAppContext;
        private readonly IMapper _mapper;

        public LogsController(FarmAppContext farmAppContext, IMapper mapper)
        {
            _farmAppContext = farmAppContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogDto>>> GetLogs(CancellationToken cancellationToken = default)
        {
            var logs = await _farmAppContext.Logs.AsNoTracking().OrderByDescending(x => x.Id).Take(500).ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<LogDto>>(logs));
        }
    }
}
