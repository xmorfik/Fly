namespace Fly.Core.Parameters
{
    public class PassengerParameter
    {
        public string? UserId { get; set; }

        public string? OrderBy { get; set; } = "Id";

        public bool Descresing { get; set; } = false;
    }
}
