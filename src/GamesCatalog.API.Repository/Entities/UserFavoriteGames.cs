namespace GamesCatalog.API.Repository.Entities
{
    public class UserFavoriteGames
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long GameId { get; set; }
        public Game Game { get; set; }
    }
}
