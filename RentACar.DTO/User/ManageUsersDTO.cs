namespace RentACar.DTO.User
{
    public class ManageUsersDTO
    {
        public ICollection<UsersDTO> Users { get; set; } = new HashSet<UsersDTO>();
        public ICollection<RoleDTO> Roles { get; set; } = new HashSet<RoleDTO>();
    }
}
