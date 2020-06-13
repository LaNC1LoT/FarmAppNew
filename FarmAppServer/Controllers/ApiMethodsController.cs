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
using FarmAppServer.Models.ApiMethods;
using FarmAppServer.Models.Sales;
using FarmAppServer.Services;
using FarmAppServer.Services.Paging;
using Microsoft.AspNetCore.Authorization;
using ServiceStack;

namespace FarmAppServer.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiMethodsController : ControllerBase
    {
        private readonly FarmAppContext _context;
        private readonly IApiMethodService _apiMethodService;
        private readonly IMapper _mapper;

        public ApiMethodsController(FarmAppContext context, IApiMethodService apiMethodService, IMapper mapper)
        {
            _context = context;
            _apiMethodService = apiMethodService;
            _mapper = mapper;
        }

        // GET: api/ApiMethods
        [HttpGet]
        public ActionResult<IEnumerable<ApiMethodDto>> GetApiMethods([FromQuery]int page = 1, [FromQuery]int pageSize = 20)
        {
            var apiMethod = _apiMethodService.GetApiMethodsDto();
            var result = apiMethod.GetPaged(page, pageSize);

            return Ok(result);
        }

        // GET: api/ApiMethods/5
        [HttpGet("ApiMethodById")]
        public async Task<ActionResult<ApiMethodDto>> GetApiMethod([FromQuery]int id)
        {
            var apiMethod = await _apiMethodService.GetApiMethodByIdAsync(id);

            return Ok(apiMethod);
        }

        // PUT: api/ApiMethods/5
        [HttpPut]
        public async Task<IActionResult> Update([FromQuery]int id, [FromBody]UpdateApiMethodDto apiMethodToUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var updated = await _apiMethodService.UpdateApiMethodAsync(id, apiMethodToUpdate);

            if (updated)
                return Ok();

            return NotFound(new ResponseBody()
            {
                Header = "Error",
                Result = "user not found"
            });
        }

        // POST: api/ApiMethods
        [HttpPost]
        public async Task<ActionResult<ApiMethod>> PostApiMethod(PostApiMethodDto model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var apiMethod = _mapper.Map<ApiMethod>(model);
            var request = await _apiMethodService.PostApiMethodAsync(apiMethod);
            var result = _mapper.Map<ApiMethodDto>(request);

            return Created("PostApiMethod", result);
        }

        // DELETE: api/ApiMethods/5
        [HttpDelete]
        public async Task<ActionResult<ApiMethod>> DeleteApiMethod(int id)
        {
            if (await _apiMethodService.DeleteSaleAsync(id))
                return Ok();

            return NotFound("Sale not found");
        }


        ///////Search!!!!!!
    //    [HttpGet("Search")]
    //    public async Task<ActionResult<ApiMethodDto>> SearchApiMethod(string param, bool? isNotNullParam, bool? isNeedAuthentication, bool? isDeleted)
    //    {
    //        if (isNotNullParam != null)
    //        {
    //            var result = await _apiMethodService.CheckForNullParam(isNotNullParam);
    //        }

    //        if (string.IsNullOrEmpty(param))
    //            return BadRequest($"Value cannot be null or empty. {nameof(param)}");

    //        var result = await _apiMethodService.SearchAsync(param, isNotNullParam, isNeedAuthentication, isDeleted);

    //        return Ok(result);
    //    }
    }
}
