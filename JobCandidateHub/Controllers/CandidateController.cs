using JobCandidateHub.Services.Interfaces;
using JobCandidateHub.Services.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobCandidateHub.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate([FromBody] CandidateViewModel model)
        {
            var result = await _candidateService.CreateOrUpdateCandidateAsync(model);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult hello()
        {
            return Ok("Hello World");
        }
    }
}
