using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using RentACar.DTO.Result;
using RentACar.DTO.Role;
using RentACar.DTO.User;
using RentACar.Web.ViewModels.Role;
using RentACar.Web.ViewModels.User;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;

namespace RentACar.Web.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IMapper mapperService;
        private readonly IAdminService adminService;

        public UserController(IMapper _mapperService, 
            IAdminService _adminService)
        {
            mapperService = _mapperService;
            adminService = _adminService;
        }
        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> SetUserRole([FromBody] SetRoleViewModel model)
        {
            SetRoleDTO dto = mapperService.Map<SetRoleDTO>(model);
            Result result = await adminService.SetRoleToUser(mapperService.Map<SetRoleDTO>(model), User);

            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(new { status = result.Message });
        }

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> RemoveUserRole([FromBody] SetRoleViewModel model)
        {
            SetRoleDTO dto = mapperService.Map<SetRoleDTO>(model);

            Result result = await adminService.DeleteRoleFromUser(dto, User);

            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(new { status = result.Message });
        }

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> RemoveUser([FromBody] DeleteUserViewModel model)
        {

            DeleteUserDTO dto = mapperService.Map<DeleteUserDTO>(model);

            Result result = await adminService.DeleteUser(dto, User);

            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(new { status = result.Message });
        }
    }
}
