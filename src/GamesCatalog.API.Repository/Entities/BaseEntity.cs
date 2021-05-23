using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesCatalog.API.Repository.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset Updated { get; set; }
    }
}
