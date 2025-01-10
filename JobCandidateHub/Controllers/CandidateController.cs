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

        [HttpGet]
        public string hello()
        {
            return "Hello World";
        }
        [HttpGet]
        public async Task<IActionResult> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Please enter Email Address");
            }
            var result = await _candidateService.GetCandidateByEmailAsync(email);
            if (result == null)
            {
                return BadRequest("Candidate with Email Address not found");
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate([FromBody] CandidateViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var result = await _candidateService.CreateOrUpdateCandidateAsync(model);
            if (result == null)
            {
                return BadRequest("Fail to Create/Update Candidate. Email required. ");
            }
            return Ok(result);
        }
    }
}
