namespace GamesCatalog.Core.Models
{
    public class GameSearchParams
    {
        public string Query { get; private set; }
        public string Sort { get; private set; }

        public GameSearchParams(string q, string sort)
        {
            Query = q;
            Sort = sort;
        }
    }
}
