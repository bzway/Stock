using System;

namespace Shared.Entity
{
    public class BaseEntity : Entity<string>
    {
        public BaseEntity()
        {
            this.CreatedOn = DateTime.Now;
            this.UpdatedOn = DateTime.Now;
        }
    }
    public class Entity<T> : IEntity<T>
    {
        public T Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
    public interface IEntity<T>
    {
        DateTime CreatedOn { get; set; }
        T Id { get; set; }
        DateTime UpdatedOn { get; set; }
    }
}