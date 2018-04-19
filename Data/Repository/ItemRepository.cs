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

        public async Task<ResponseObject<object>> AddItem(ItemViewModel itemViewModel)
        {
            var response = new ResponseObject<object>();

            try
            {
                InventoryItem item = new InventoryItem
                {
                    InventoryItemId = Guid.NewGuid().ToString(),
                    BarCode = Guid.NewGuid().ToString(),
                    CategoryId = AppDbContext.Categories.FirstOrDefault(a=>a.Name.Contains(itemViewModel.Category.Name))?.CategoryId,
                    Description = itemViewModel.Description,
                    Value = itemViewModel.Value,
                    OrderNumber = AppDbContext.InventoryItems.Count() + 1
                };

                await AppDbContext.InventoryItems.AddAsync(item);
                await AppDbContext.SaveChangesAsync();

                var itemVm = new ItemViewModel
                {
                    InventoryItemId = item.InventoryItemId,
                    BarCode = item.BarCode,
                    CategoryId = item.CategoryId,
                    Description = item.Description,
                    OrderNumber = item.OrderNumber,
                    Value = item.Value,
                    Category = new CategoryViewModel
                    {
                        Name = item.Category.Name,
                        CategoryId = item.Category.CategoryId
                    }
                };

                response.Message = "Item is successfully added!";
                response.Data = itemVm;
                response.Success = true;

                return response;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
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

        public async Task<ResponseObject<object>> UpdateItem(ItemViewModel itemViewModel)
        {
            var response = new ResponseObject<object>();

            try
            {
                var item = await AppDbContext.InventoryItems
                                .Include(a=>a.Category)
                                    .FirstOrDefaultAsync(a => a.InventoryItemId == itemViewModel.InventoryItemId);

                if (item == null)
                {
                    response.Message = "Can't find item";
                    response.Success = false;
                    return response;
                }
                
                item.Value = itemViewModel.Value;
                item.BarCode = itemViewModel.BarCode;
                item.Description = itemViewModel.Description;
                item.CategoryId = AppDbContext.Categories.FirstOrDefault(a => a.Name.Contains(itemViewModel.Category.Name))?.CategoryId;

                AppDbContext.InventoryItems.Update(item);
                await AppDbContext.SaveChangesAsync();
                

                var itemVm = new ItemViewModel
                {
                    BarCode = item.BarCode,
                    CategoryId = item.CategoryId,
                    Description = item.Description,
                    InventoryItemId = item.InventoryItemId,
                    OrderNumber = item.OrderNumber,
                    Value = item.Value,
                    Category = new CategoryViewModel
                    {
                        Name = item.Category.Name,
                        CategoryId = item.Category.CategoryId
                    }
                };
                
                response.Data = itemVm;
                response.Success = true;
                response.Message = "Item is successfully updated!";
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<ResponseObject<object>> DeleteItemAsync(string itemId)
        {
            var response = new ResponseObject<object>();

            try
            {
                var item = await AppDbContext.InventoryItems
                                .FirstOrDefaultAsync(a => a.InventoryItemId == itemId);

                if (item == null)
                {
                    response.Message = "Can't find item";
                    response.Success = false;
                    return response;
                }

                AppDbContext.InventoryItems.Remove(item);
                await AppDbContext.SaveChangesAsync();

                response.Success = true;
                response.Message = "Item is successfully deleted!";
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }
    }
}
