using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesCatalog.API.Repository.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(350)]
        public string Email { get; set; }
        
        [MaxLength(75)]
        public string FirstName { get; set; }

        [MaxLength(75)]
        public string LastName { get; set; }

        public bool? Verified { get; set; } = null;
        public bool IsLocked { get; set; } = false;
        public ICollection<UserFavoriteGames> FavoriteGames { get; set; }
    }
}
