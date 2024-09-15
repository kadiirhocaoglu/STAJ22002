using HST.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HST.DTO.DTOs.UserDtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string WebSiteAddress { get; set; }
        public bool UserConfirmed { get; set; }
        public int AccessFailedCount { get; set; }

        public List<CustomerAccount> CustomerAccounts = [];
        public string Role { get; set; }
        public bool IsLiveSupport { get; set; }
    }
}
