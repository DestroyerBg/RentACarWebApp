using RentACar.DTO.User;

namespace RentACar.Web.ViewModels.User
{
    public class UserViewModel
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;

        public ICollection<RoleViewModel> UserRoles = new HashSet<RoleViewModel>();
    }
}
