﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Company
    {
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime ValidLicenceTill { get; set; }
        public string ContactPerson { get; set; }
        public List<Category> Categories { get; set; }
    }
}
