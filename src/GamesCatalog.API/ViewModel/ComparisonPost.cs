using System.Linq;
using System.Text.Json.Serialization;

namespace GamesCatalog.API.ViewModel
{
    public class ComparisonPost
    {
        private string[] PossibleComparisons = new string[] { "union", "intersection", "difference" };

        [JsonPropertyName("otherUserId")]
        public long OtherUserId { get; set; }

        [JsonPropertyName("comparison")]
        public string Comparison { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Comparison)) return false;
            if (!PossibleComparisons.Contains(Comparison.ToLowerInvariant())) return false;
            if (OtherUserId < 1) return false;

            return true;
        }
    }
}
