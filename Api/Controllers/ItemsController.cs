using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Core;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

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

        [HttpPost("items/add")]
        public Task<ResponseObject<object>> AddItem([FromBody] ItemViewModel itemViewModel)
        {
            var result = _repositoryWrapper.Items.AddItem(itemViewModel);

            return result;
        }

        [HttpGet("items/list/{categoryId}")]
        public ResponseObject<object> GetItems(string categoryId)
        {
            var userId = GetUserIdFromToken();

            var result = _repositoryWrapper.Items.GetItems(userId, categoryId);

            return result;
        }

        [HttpPut("items/update")]
        public Task<ResponseObject<object>> UpdateItem([FromBody] ItemViewModel itemViewModel)
        {
            var result = _repositoryWrapper.Items.UpdateItem(itemViewModel);

            return result;
        }


        [HttpDelete("items/delete/{itemId}")]
        public Task<ResponseObject<object>> DeleteItem(string itemId)
        {
            var result = _repositoryWrapper.Items.DeleteItemAsync(itemId);

            return result;
        }
    }
}
