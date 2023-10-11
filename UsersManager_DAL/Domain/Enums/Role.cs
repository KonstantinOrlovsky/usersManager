using System.Text.Json.Serialization;

namespace UsersManager_DAL.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        User,
        Admin,
        Support,
        SuperAdmin
    }
}