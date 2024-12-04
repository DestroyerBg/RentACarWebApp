namespace RentACar.DTO.User
{
    public class UsersDTO
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;

        public ICollection<RoleDTO> UserRoles = new HashSet<RoleDTO>();
    }
}
