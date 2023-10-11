namespace UsersManager_DAL.Domain
{
    public interface IEntity<TIdentifier>
    {
        TIdentifier Id { get; set; }
    }
}