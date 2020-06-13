using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly FarmAppContext _context;
        public ReportsController(FarmAppContext context)
        {
            _context = context;
        }

        [HttpPost("[action]")]
        public IActionResult ReportPrice(DragPriceRequest dragPriceRequest)
        {
            //var pharmacy = new List<Pharmacy>();
            //var s = _context.Sales.Where(x => x.SaleDate > dragPriceRequest.From && x.SaleDate < dragPriceRequest.To)
            //    .GroupJoin(dragPriceRequest, s => s.PharmacyId, x, (s, x) => s);
            return Ok();
        }
    }

    public class DragPriceRequest
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int[] PharmacyId { get; set; }

    }
}
