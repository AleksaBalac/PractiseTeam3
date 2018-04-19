using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/")]
    public class ItemsController : BaseController
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ItemsController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet("items/list/{categoryId}")]
        public ResponseObject<object> GetItems(string categoryId)
        {
            var userId = GetUserIdFromToken();

            var result = _repositoryWrapper.Items.GetItems(userId, categoryId);

            return result;
        }
    }
}
