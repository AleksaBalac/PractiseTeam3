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
        public async Task<IActionResult> AddItem([FromBody] ItemViewModel itemViewModel)
        {
            var result = await _repositoryWrapper.Items.AddItem(itemViewModel);

            if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("items/list/{categoryId}")]
        public async Task<IActionResult> GetItems(string categoryId)
        {
            var userId = GetUserIdFromToken();

            var result = await _repositoryWrapper.Items.GetItems(userId, categoryId);

            if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);
            
            return Ok(result);
        }

        [HttpPut("items/update")]
        public async Task<IActionResult> UpdateItem([FromBody] ItemViewModel itemViewModel)
        {
            var result = await _repositoryWrapper.Items.UpdateItem(itemViewModel);

            if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

            return Ok(result);
        }


        [HttpDelete("items/delete/{itemId}")]
        public async Task<IActionResult> DeleteItem(string itemId)
        {
            var result = await _repositoryWrapper.Items.DeleteItemAsync(itemId);

            if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

            return Ok(result);
        }
    }
}
