using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using ViewModels;

namespace Contracts
{
    public interface ICompanyRepository
    {
        Task<ResponseObject<object>> AddCompanyAsync(CompanyViewModel companyViewModel);
        Task<ResponseObject<object>> GetCompanies();
        Task<ResponseObject<object>> UpdateCompany(CompanyViewModel companyViewModel);
        Task<ResponseObject<object>> DeleteCompany(string companyId);
    }
}
