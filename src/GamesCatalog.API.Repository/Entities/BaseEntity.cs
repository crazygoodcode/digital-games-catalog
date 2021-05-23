using System;
using System.ComponentModel.DataAnnotations;

namespace GamesCatalog.API.Repository.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
    }
}
