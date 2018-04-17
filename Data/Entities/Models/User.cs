
using Microsoft.AspNetCore.Identity;
using Microsoft.WindowsAzure.Storage.Table;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [IgnoreProperty]
        public string FullName => $"{FirstName} {LastName}";
        public string Phone { get; set; }
    }
}
