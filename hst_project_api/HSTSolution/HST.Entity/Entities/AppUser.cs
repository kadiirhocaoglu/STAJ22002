using Microsoft.AspNetCore.Identity;

namespace HST.Entity.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string WebSiteAddress { get; set; }
        public bool UserConfirmed { get; set; }
        public List<CustomerAccount> CustomerAccounts { get; set; }

        public bool IsLiveSupport { get; set; }

    }
}
