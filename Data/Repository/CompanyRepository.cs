using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Core;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ViewModels;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        private readonly UserManager<User> _userManager;

        public CompanyRepository(AppDbContext appDbContext, UserManager<User> userManager)
            : base(appDbContext)
        {
            _userManager = userManager;
        }

        public async Task<ResponseObject<object>> AddCompanyAsync(CompanyViewModel companyViewModel)
        {
            var response = new ResponseObject<object>();

            try
            {
                Company company = new Company
                {
                    CompanyId = Guid.NewGuid().ToString(),
                    Name = companyViewModel.Name,
                    Address = companyViewModel.Address,
                    ContactPerson = companyViewModel.ContactPerson,
                    ValidLicenceTill = companyViewModel.ValidLicenceTill
                };

                await AppDbContext.Companies.AddAsync(company);

                User user = new User
                {
                    FirstName = companyViewModel.CompanyAdmin.FirstName,
                    LastName = companyViewModel.CompanyAdmin.LastName,
                    Email = companyViewModel.CompanyAdmin.Email
                };

                await _userManager.CreateAsync(user, "Pass1234");
                await _userManager.AddToRoleAsync(user, "CompanyAdmin");

                CompanyAccount companyAccount = new CompanyAccount
                {
                    CompanyId = company.CompanyId,
                    Company = company,
                    UserId = user.Id,
                    User = user
                };

                await AppDbContext.CompanyAccount.AddAsync(companyAccount);

                await AppDbContext.SaveChangesAsync();

                response.Data = null;
                response.Message = "Company successfully added!";
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = StatusCode.BadRequest;
                response.Message = e.Message;
                return response;
            }
        }

        public async Task<ResponseObject<object>> GetCompanies()
        {
            var response = new ResponseObject<object>();

            try
            {
                var companies = await AppDbContext.Companies.OrderBy(a => a.Name).ToListAsync();

                List<CompanyViewModel> companiesViewModel = new List<CompanyViewModel>();

                foreach (var company in companies)
                {
                    CompanyViewModel companyViewModel = new CompanyViewModel
                    {
                        Name = company.Name,
                        Address = company.Address,
                        ContactPerson = company.ContactPerson,
                        ValidLicenceTill = company.ValidLicenceTill,
                        CompanyId = company.CompanyId
                    };

                    companiesViewModel.Add(companyViewModel);
                }

                response.Data = companiesViewModel;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = StatusCode.BadRequest;
                response.Message = e.Message;
                return response;
            }
        }

        public async Task<ResponseObject<object>> UpdateCompany(CompanyViewModel companyViewModel)
        {
            var response = new ResponseObject<object>();

            try
            {
                var company = await AppDbContext.Companies.FirstOrDefaultAsync(a => a.CompanyId == companyViewModel.CompanyId);

                if (company == null)
                {
                    response.Message = "Can't find company";
                    response.StatusCode = StatusCode.BadRequest;
                    return response;
                }

                company.Name = companyViewModel.Name;
                company.Address = companyViewModel.Address;
                company.ContactPerson = companyViewModel.ContactPerson;
                company.ValidLicenceTill = companyViewModel.ValidLicenceTill;

                await AppDbContext.SaveChangesAsync();

                response.Data = companyViewModel;
                response.Message = "Company successfully updated!";
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = StatusCode.BadRequest;
                response.Message = e.Message;
                return response;
            }
        }

        public async Task<ResponseObject<object>> DeleteCompany(string companyId)
        {
            var response = new ResponseObject<object>();

            try
            {
                var company = await AppDbContext.Companies.FirstOrDefaultAsync(a => a.CompanyId == companyId);

                if (company == null)
                {
                    response.Message = "Can't find company";
                    response.StatusCode = StatusCode.BadRequest;
                    return response;
                }

                var companyAccounts = await AppDbContext.CompanyAccount.Include(a => a.User).Where(a => a.CompanyId == companyId).ToListAsync();
                
                AppDbContext.CompanyAccount.RemoveRange(companyAccounts);

                foreach (var account in companyAccounts)
                {
                    await _userManager.DeleteAsync(account.User);
                }

                AppDbContext.Companies.Remove(company);

                await AppDbContext.SaveChangesAsync();

                response.Data = null;
                response.Message = "Company successfully updated!";
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = StatusCode.BadRequest;
                response.Message = e.Message;
                return response;
            }
        }
    }
}
