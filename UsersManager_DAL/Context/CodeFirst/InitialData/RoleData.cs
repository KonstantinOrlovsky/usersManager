using UsersManager_DAL.Domain;

namespace UsersManager_DAL.Context.CodeFirst.InitialData
{
    public class RoleData
    {
        public static readonly Role User = new()
        {
            Id = new Guid("331a054c-85d3-4a8e-a8e9-0cc677a9041c"),
            Name = "User"
        };

        public static readonly Role Admin = new()
        {
            Id = new Guid("6ff039b9-b373-4443-a7ed-0cea1a341f2f"),
            Name = "Admin"
        };

        public static readonly Role Support = new()
        {
            Id = new Guid("94ba79e1-ca12-4669-a1bd-94a8a2e5c8da"),
            Name = "Support"
        };

        public static readonly Role SuperAdmin = new()
        {
            Id = new Guid("b4200fb1-be7c-48bb-adfa-dc8abead32d9"),
            Name = "SuperAdmin"
        };
    }
}