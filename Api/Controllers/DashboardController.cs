using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/")]
    public class DashboardController : BaseController
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public DashboardController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet("dashboard/company")]
        public async Task<IActionResult> GetCompanyDetails()
        {
            try
            {
                var userId = GetUserIdFromToken();

                var result = await _repositoryWrapper.Dashboard.GetCompanyDetails(userId);

                if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("dashboard/category")]
        public async Task<IActionResult> GetCategoryDetails()
        {
            try
            {
                var userId = GetUserIdFromToken();

                var result = await _repositoryWrapper.Dashboard.GetCategoryDetails(userId);

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
