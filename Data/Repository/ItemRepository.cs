using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Core;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ViewModels;

namespace Repository
{
    public class ItemRepository : RepositoryBase<InventoryItem>, IItemRepository
    {
        public ItemRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
        }

        public ResponseObject<object> GetItems(string userId, string categoryId)
        {
            var response = new ResponseObject<object>();
            try
            {
                CompanyAccount companyAccount = AppDbContext.CompanyAccount.Include(a => a.Company).ThenInclude(a => a.Categories)
                    .ThenInclude(a => a.Items).FirstOrDefault(a => a.UserId == userId);

                if (companyAccount == null)
                {
                    response.Success = false;
                    response.Message = "Can't find logged in company";
                    return response;
                }

                Category category = companyAccount.Company.Categories.FirstOrDefault(a => a.CategoryId == categoryId);

                if (category == null)
                {
                    response.Success = false;
                    response.Message = "Can't find category";
                    return response;
                }

                var itemVm = new List<ItemViewModel>();

                foreach (var inventoryItem in category.Items)
                {
                    var categoryViewModel = new CategoryViewModel
                    {
                        Name = category.Name,
                        CategoryId = category.CategoryId
                    };

                    var itemViewModel = new ItemViewModel
                    {
                        Category = categoryViewModel,
                        Value = inventoryItem.Value,
                        CategoryId = inventoryItem.CategoryId,
                        BarCode = inventoryItem.BarCode,
                        Description = inventoryItem.Description,
                        InventoryItemId = inventoryItem.InventoryItemId,
                        OrderNumber = inventoryItem.OrderNumber
                    };

                    itemVm.Add(itemViewModel);
                }


                response.Data = itemVm;
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
