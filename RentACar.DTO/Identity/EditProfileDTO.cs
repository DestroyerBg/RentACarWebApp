namespace RentACar.DTO.Identity
{
    public class EditProfileDTO
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string BirthDate { get; set; } = null!;

    }
}
