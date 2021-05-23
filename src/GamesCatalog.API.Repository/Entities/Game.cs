using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GamesCatalog.API.Repository.Entities
{
    [Table("Games")]
    public class Game : BaseEntity
    {
        [Required]
        public long ExternalId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        public int Added { get; set; }
        public ushort MetacriticScore { get; set; }
        public float Rating { get; set; }
        public string Released { get; set; }
        public DateTime? LastSync { get; set; }
    }
}
