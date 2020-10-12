using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Launchpad.App.Repositories.Interfaces;
using Launchpad.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Launchpad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;

        }

        [HttpPost]
        public async Task<ActionResult<CompanyVM>> Create([FromBody] CompanyCreateVM data)
        {
            //make sure model has all required fields
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");

            try
            {
                var result = await _companyRepository.Create(data);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }

        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyVM>>> GetAll()
        {
            try
            {
                var results = await _companyRepository.GetAll();
                return Ok(results);
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
