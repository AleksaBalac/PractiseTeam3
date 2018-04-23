using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace Api.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CompanyController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

    }
}
