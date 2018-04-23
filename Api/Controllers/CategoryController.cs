using System;
using System.Threading.Tasks;
using Contracts;
using Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ViewModels;

namespace Api.Controllers
{
    [Route("api/")]
    public class CategoryController : BaseController
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CategoryController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet("category/list")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var userId = GetUserIdFromToken();

                var result = await _repositoryWrapper.Category.GetCategoryList(userId);

                if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        [HttpPost("category/add")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryViewModel category)
        {
            try
            {
                var userId = GetUserIdFromToken();

                var result = await _repositoryWrapper.Category.AddCategory(userId, category);

                if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        [HttpPut("category/update")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryViewModel category)
        {
            try
            {
                var result = await _repositoryWrapper.Category.UpdateCategory(category);

                if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpDelete("category/delete/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(string categoryId)
        {
            try
            {
                var result = await _repositoryWrapper.Category.DeleteCategory(categoryId);

                if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
