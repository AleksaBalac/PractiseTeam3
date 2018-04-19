using Core;

namespace Contracts
{
    public interface IItemRepository
    {
        ResponseObject<object> GetItems(string userId,string categoryId);
    }
}
