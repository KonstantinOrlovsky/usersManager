namespace UsersManager_DAL.Domain
{
    public class User : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}