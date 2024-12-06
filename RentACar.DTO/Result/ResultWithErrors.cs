namespace RentACar.DTO.Result
{
    public class ResultWithErrors
    {
        public bool Success { get; set; }

        public ICollection<string> Errors { get; set; } = new HashSet<string>();
    }
}
