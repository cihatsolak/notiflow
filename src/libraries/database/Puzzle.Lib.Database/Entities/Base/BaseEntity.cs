﻿namespace Puzzle.Lib.Database.Entities.Base
{
    public abstract class BaseEntity : IEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public int Id { get; set; }
    }
}