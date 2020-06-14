using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using FarmAppServer.Models.CodeAthTypes;
using FarmAppServer.Models.Sales;
using FarmAppServer.Services;
using FarmAppServer.Services.Paging;
using ServiceStack;

namespace FarmAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeAthTypesController : ControllerBase
    {
        private readonly FarmAppContext _context;
        private readonly IMapper _mapper;
        private readonly ICodeAthService _codeAthService;

        public CodeAthTypesController(FarmAppContext context, IMapper mapper, ICodeAthService codeAthService)
        {
            _context = context;
            _mapper = mapper;
            _codeAthService = codeAthService;
        }

        // GET: api/CodeAthTypes
        [HttpGet]
        public ActionResult<IEnumerable<CodeAthTypeDto>> GetCodeAthTypes([FromQuery]int page = 1, [FromQuery]int pageSize = 25)
        {
            var codesAthType = _codeAthService.GetCodeAthTypes();

            var result = codesAthType.GetPaged(page, pageSize);

            if (result == null)
                return NotFound("CodeAthType not found");

            return Ok(result);
        }

        // GET: api/CodeAthTypes/5
        [HttpGet("CodeAthById")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<CodeAthType>> GetCodeAthType([FromForm]int key)
        {
            if (key <= 0) return BadRequest("Key must be > 0");

            var codeAthType = await _codeAthService.GetCodeAthTypeById(key);

            if (codeAthType == null)
                return NotFound("CodeAthType not found");

            return Ok(codeAthType);
        }

        // PUT: api/CodeAthTypes/5
        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PutCodeAthType([FromForm]int key, [FromForm]string values)
        {
            if (key <= 0) return BadRequest("key must be > 0");
            if (string.IsNullOrEmpty(values)) return BadRequest("Value cannot be null or empty");

            var updated = await _codeAthService.UpdateCodeAthTypeAsync(key, values);

            if (updated) return Ok();

            return NotFound(new ResponseBody()
            {
                Header = "Error",
                Result = "CodeAthType not found or nothing to update"
            });
        }

        // POST: api/CodeAthTypes
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<CodeAthType>> PostCodeAthType([FromForm]string values)
        {
            if (string.IsNullOrEmpty(values)) return BadRequest("Value cannot be null or empty");

            var request = await _codeAthService.PostCodeAthTypeAsync(values);

            if (request) return Ok();

            return BadRequest();
        }

        // DELETE: api/CodeAthTypes/5
        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<CodeAthType>> DeleteCodeAthType([FromForm]int key)
        {
            if (key <= 0) return BadRequest("key cannot be <= 0");
            if (await _codeAthService.DeleteCodeAthTypeAsync(key)) return Ok();

            return NotFound(new ResponseBody()
            {
                Header = "Error",
                Result = "CodeAthType not found"
            });
        }
    }
}
