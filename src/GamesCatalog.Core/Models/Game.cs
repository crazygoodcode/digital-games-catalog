using System.Text.Json.Serialization;

namespace GamesCatalog.Core.Models
{
    public class Game
    {
        [JsonPropertyName("gameId")]
        public long GameId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("added")]
        public int Added { get; set; }

        [JsonPropertyName("metacritic")]
        public int Metacritic { get; set; }

        [JsonPropertyName("rating")]
        public float Rating { get; set; }

        [JsonPropertyName("released")]
        public string Released { get; set; }

        [JsonPropertyName("updated")]
        public string Updated { get; set; }
    }
}
