using System;

namespace GamesCatalog.API.Repository.Entities
{
    public abstract class BaseEntity
    {
        public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;
    }
}
