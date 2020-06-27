using FarmApp.Domain.Core.StoredProcedure;
using FarmApp.Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FarmAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly FarmAppContext _farmAppContext;
        public ChartsController(FarmAppContext farmAppContext)
        {
            _farmAppContext = farmAppContext;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ChartSale>>> Sales(CancellationToken cancellationToken = default)
        {
            return Ok(await _farmAppContext.ChartSales.FromSqlRaw("EXECUTE dbo.ChartSales").AsNoTracking().ToListAsync(cancellationToken));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ChartSale>>> Stocks(CancellationToken cancellationToken = default)
        {
            return Ok(await _farmAppContext.ChartStocks.FromSqlRaw("EXECUTE dbo.ChartStock").AsNoTracking().ToListAsync(cancellationToken));
        }
    }
}
