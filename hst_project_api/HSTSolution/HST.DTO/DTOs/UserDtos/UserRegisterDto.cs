using HST.Entity.Entities;


namespace HST.DTO.DTOs.UserDtos
{
    public class UserRegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string WebSiteAddress { get; set; }
        public string Password { get; set; }
        public int RoleId  { get; set; } = 2;
                public List<AppRole> Roles { get; set; } 
        public List<CustomerAccount> CustomerAccounts = [];
        public bool UserConfirmed = false;
       
        public bool IsLiveSupport = false;

    }
}
