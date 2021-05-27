using GamesCatalog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace GamesCatalog.Core.Integrations.Impl
{
    class ServiceSearchResult
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("previous")]
        public string Prev { get; set; }

        [JsonPropertyName("results")]
        public List<ServiceGame> Results { get; set; }
    }

    class ServiceGame
    {
        [JsonPropertyName("id")]
        public long GameId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("added")]
        public int? Added { get; set; }

        [JsonPropertyName("metacritic")]
        public int? Metacritic { get; set; }

        [JsonPropertyName("rating")]
        public float Rating { get; set; }

        [JsonPropertyName("released")]
        public string Released { get; set; }

        [JsonPropertyName("updated")]
        public string Updated { get; set; }

        public Game Map()
        {
            return new Game
            {
                GameId = GameId,
                Name = Name,
                Added = Added ?? -1,
                Metacritic = Metacritic ?? -1,
                Rating = Rating,
                Released = Released,
                Updated = Updated
            };
        }
    }

    public sealed class GameDataAPIService : IGameDataAPIService, IDisposable
    {
        private readonly IntegrationConfiguration _integrationConfiguration;
        private readonly WebClient _client; // HttpClient and factory would be better for headers etc but this will do the trick for now.
        private bool _disposed = false;

        public GameDataAPIService(IntegrationConfiguration integrationConfiguration)
        {
            _integrationConfiguration = integrationConfiguration;
            _client = new WebClient();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isDisposing)
        {
            if (_disposed) return;
            
            if (isDisposing)
            {
                _client.Dispose();
                _disposed = true;
            }
        }

        public Task<Game> GetAsync(long gameId, CancellationToken cancellationToken = default)
        {
            var json = _client.DownloadString($"https://api.rawg.io/api/games/{gameId}?key={_integrationConfiguration.Key}");
            var game = JsonSerializer.Deserialize<ServiceGame>(json);

            return Task.FromResult<Game>(game.Map());
        }

        public Task<IEnumerable<Game>> SearchAsync(GameSearchParams query, CancellationToken cancellationToken = default)
        {
            var json = _client.DownloadString($"https://api.rawg.io/api/games?search={query.Query}&ordering={query.Sort}&key={_integrationConfiguration.Key}");
            var result = JsonSerializer.Deserialize<ServiceSearchResult>(json);

            if (result != null)
            {
                if (!string.IsNullOrEmpty(result.Next))
                {
                    // We need to decide if we allow paging at the api level and pass down the next or build the resultset here
                }

                return Task.FromResult(result.Results.Select(s => s.Map()).AsEnumerable());
            }

            return null;
        }
    }
}
