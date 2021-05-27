using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GamesCatalog.Core.Models
{
    public class User
    {
        [JsonPropertyName("userId")]
        public long UserId { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("games")]
        public List<Game> Games { get; set; }
    }
}
