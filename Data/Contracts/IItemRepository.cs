using System.Threading.Tasks;
using Core;
using ViewModels;

namespace Contracts
{
    public interface IItemRepository
    {
        Task<ResponseObject<object>> AddItem(ItemViewModel itemViewModel);
        ResponseObject<object> GetItems(string userId,string categoryId);
        Task<ResponseObject<object>> UpdateItem(ItemViewModel itemViewModel);
        Task<ResponseObject<object>> DeleteItemAsync(string itemId);
    }
}
