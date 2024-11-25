namespace RentACar.DTO.Identity
{
    public class ChangePasswordDTO
    {
        public string OldPassword { get; set; } = null!;

        public string NewPassword { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;
    }
}
