using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Company
    {
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime ValidLicenceTill { get; set; }
        public string ContactPerson { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
