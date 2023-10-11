using System.Text.Json.Serialization;
using UsersManager_DAL.Domain.Enums;

namespace UsersManager_DAL.Domain.Filter
{
    public class UserFilter
    {
        public string Name { get; set; } = string.Empty;
        public int? Age { get; set; }
        public string Email { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public List<Enums.Role>? Roles { get; set; }
    }
}