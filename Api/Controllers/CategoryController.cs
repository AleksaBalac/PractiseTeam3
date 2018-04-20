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
        public Task<ResponseObject<object>> GetCategories()
        {
            var userId = GetUserIdFromToken();

            var result = _repositoryWrapper.Category.GetCategoryList(userId);

            return result;
        }

        [HttpPost("category/add")]
        public Task<ResponseObject<object>> AddCategory([FromBody] CategoryViewModel category)
        {
            var userId = GetUserIdFromToken();

            var result = _repositoryWrapper.Category.AddCategory(userId, category);

            return result;
        }

        [HttpPut("category/update")]
        public Task<ResponseObject<object>> UpdateCategory([FromBody] CategoryViewModel category)
        {
            var result = _repositoryWrapper.Category.UpdateCategory(category);

            return result;
        }

        [HttpDelete("category/delete/{categoryId}")]
        public Task<ResponseObject<object>> DeleteCategory(string categoryId)
        {
            var result = _repositoryWrapper.Category.DeleteCategory(categoryId);

            return result;
        }

    }
}
