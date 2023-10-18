namespace UsersManager_BAL.Models
{
    public class RegisterModel
    {
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public required string Email { get; set; }
    }
}