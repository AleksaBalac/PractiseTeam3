using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace Api.Controllers
{
    [Route("api/")]
    public class CompanyController : BaseController
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CompanyController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpPost("company/add")]
        public async Task<IActionResult> AddCompany([FromBody] CompanyViewModel companyViewModel)
        {
            try
            {
                var result = await _repositoryWrapper.Company.AddCompanyAsync(companyViewModel);

                if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("company/list")]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var result = await _repositoryWrapper.Company.GetCompanies();

                if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("company/update")]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyViewModel companyViewModel)
        {
            try
            {
                var result = await _repositoryWrapper.Company.UpdateCompany(companyViewModel);

                if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("company/delete/{companyId}")]
        public async Task<IActionResult> DeleteCompany(string companyId)
        {
            try
            {
                var result = await _repositoryWrapper.Company.DeleteCompany(companyId);

                if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
