using System.Threading.Tasks;
using Contracts;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CategoryController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet("category/list")]
        public Task<ResponseObject<object>> GetItems()
        {
            var userId = GetUserIdFromToken();

            var result = _repositoryWrapper.Category.GetCategoryList(userId);

            return result;
        }
    }
}
