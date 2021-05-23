using System;

namespace GamesCatalog.API.Repository.Entities
{
    public class UserFavoriteGames
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid GameId { get; set; }
        public Game Game { get; set; }
    }
}
