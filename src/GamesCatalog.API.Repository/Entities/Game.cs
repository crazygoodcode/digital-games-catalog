using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GamesCatalog.API.Repository.Entities
{
    [Table("Games")]
    public class Game : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long GameId { get; set; }

        [Required]
        public long ExternalId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        public int Added { get; set; }
        public ushort MetacriticScore { get; set; }
        public float Rating { get; set; }
        [MaxLength(20)]
        public string Released { get; set; }
        public DateTime? LastSync { get; set; }

        public List<User> Users { get; set; }
    }
}
