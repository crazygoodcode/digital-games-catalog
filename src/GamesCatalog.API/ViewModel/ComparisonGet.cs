using GamesCatalog.Core.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GamesCatalog.API.ViewModel
{
    public class ComparisonGet : ComparisonPost
    {
        [JsonPropertyName("userId")]
        public long UserId { get; set; }

        [JsonPropertyName("games")]
        public List<Game> Games { get; set; }
    }
}
