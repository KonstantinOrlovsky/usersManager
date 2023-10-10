namespace UsersManager_DAL.Domain
{
    public class Role : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserRole> UserRole { get; set; } = new HashSet<UserRole>();
    }
}