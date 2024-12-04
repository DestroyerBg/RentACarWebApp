namespace RentACar.Web.ViewModels.User
{
    public class ManagerUsersViewModel
    {
        public ICollection<UserViewModel> Users { get; set; } = new HashSet<UserViewModel>();
        public ICollection<RoleViewModel> Roles { get; set; } = new HashSet<RoleViewModel>();
    }
}
