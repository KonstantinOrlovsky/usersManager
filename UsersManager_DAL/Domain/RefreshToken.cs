namespace UsersManager_DAL.Domain
{
    public class RefreshToken : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public bool IsUsed { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ExpiredDate { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}