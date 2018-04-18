namespace Entities.Models
{
    public class CompanyAccount
    {
        public string CompanyAccountId { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public Company Company { get; set; }
        public string CompanyId { get; set; }
    }
}
