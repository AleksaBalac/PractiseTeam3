using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Contracts
{
    public interface IDashboardRepository
    {
        Task<ResponseObject<object>> GetCompanyDetails(string userId);
        Task<ResponseObject<object>> GetItemDetails(string userId);
        Task<ResponseObject<object>> GetCategoryDetails(string userId);
    }
}
