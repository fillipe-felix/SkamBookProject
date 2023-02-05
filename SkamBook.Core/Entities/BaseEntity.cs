﻿namespace SkamBook.Core.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}