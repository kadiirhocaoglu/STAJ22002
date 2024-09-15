using AutoMapper;
using HST.Business.Services.Abstract;
using HST.DataAccess.UnitOfWork.Abstact;
using HST.DTO.DTOs.UserDtos;
using HST.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HST.Business.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<AppRole> roleManager;

        public UserService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
       public async Task<IdentityResult> CreateUserAsync(UserRegisterDto userRegisterDto)
    {
        var map = mapper.Map<AppUser>(userRegisterDto);
        map.UserName = userRegisterDto.PhoneNumber;

        var result = await userManager.CreateAsync(map, string.IsNullOrEmpty(userRegisterDto.Password) ? "" : userRegisterDto.Password);
        
        if (result.Succeeded)
        {
            var role = await roleManager.FindByIdAsync(userRegisterDto.RoleId.ToString());
            
            if (role != null)
            {
                await userManager.AddToRoleAsync(map, role.Name);
            }
            else
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"Role with ID {userRegisterDto.RoleId} does not exist."
                });
            }
        }

        return result;
    }

        public async Task<(IdentityResult identityResult, string? email)> DeleteUserAsync(int userId)
        {
            var user = await GetAppUserByIdAsync(userId);
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
                return (result, user.Email);
            else
                return (result, null);
        }

        public async Task<List<AppRole>> GetAllRolesAsync()
        {
            return await roleManager.Roles.ToListAsync();
        }

        public async Task<List<UserDto>> GetAllUsersWithRoleAsync()
        {
            var users = await userManager.Users.ToListAsync();
            var map = mapper.Map<List<UserDto>>(users);


            foreach (var item in map)
            {
                var findUser = await userManager.FindByIdAsync(item.Id.ToString());
                var role = string.Join("", await userManager.GetRolesAsync(findUser));

                item.Role = role;
            }

            return map;
        }

        public async Task<AppUser> GetAppUserByIdAsync(int userId)
        {
            return await userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<string> GetUserRoleAsync(AppUser user)
        {
            return string.Join("", await userManager.GetRolesAsync(user));
        }

        public async Task<IdentityResult> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var user = await GetAppUserByIdAsync(userUpdateDto.Id);
            var userRole = await GetUserRoleAsync(user);

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await userManager.RemoveFromRoleAsync(user, userRole);
                var findRole = await roleManager.FindByIdAsync(userUpdateDto.RoleId.ToString());
                await userManager.AddToRoleAsync(user, findRole.Name);
                return result;
            }
            else
                return result;
        }

        public async Task<UserProfileDto> GetUserProfileAsync()
        {
            var userIdString = userManager.GetUserId(httpContextAccessor.HttpContext.User);
            int userId = int.Parse(userIdString);

            var getUserWithImage = await unitOfWork.GetRepository<AppUser>().GetAsync(x => x.Id == userId);
            var map = mapper.Map<UserProfileDto>(getUserWithImage);

            return map;
        }
        public async Task<List<UserDto>> GetLiveSupportUsersAsync()
        {
            var users = await userManager.Users.Where(u => u.IsLiveSupport).ToListAsync();
            var map = mapper.Map<List<UserDto>>(users);

            foreach (var item in map)
            {
                var findUser = await userManager.FindByIdAsync(item.Id.ToString());
                var role = string.Join("", await userManager.GetRolesAsync(findUser));

                item.Role = role;
            }

            return map;
        }



        public async Task<bool> UserProfileUpdateAsync(UserProfileDto userProfileDto)
        {
            var userIdString = userManager.GetUserId(httpContextAccessor.HttpContext.User);
            int userId = int.Parse(userIdString);
            var user = await GetAppUserByIdAsync(userId);

            var isVerified = await userManager.CheckPasswordAsync(user, userProfileDto.CurrentPassword);
            if (isVerified && userProfileDto.NewPassword != null)
            {
                var result = await userManager.ChangePasswordAsync(user, userProfileDto.CurrentPassword, userProfileDto.NewPassword);
                if (result.Succeeded)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                    await signInManager.SignOutAsync();
                    await signInManager.PasswordSignInAsync(user, userProfileDto.NewPassword, true, false);

                    mapper.Map(userProfileDto, user);


                    await userManager.UpdateAsync(user);
                    await unitOfWork.SaveAsync();

                    return true;
                }
                else
                    return false;
            }
            else if (isVerified)
            {
                await userManager.UpdateSecurityStampAsync(user);
                mapper.Map(userProfileDto, user);

                
                await userManager.UpdateAsync(user);
                await unitOfWork.SaveAsync();
                return true;
            }
            else
                return false;
        }
    }
}
