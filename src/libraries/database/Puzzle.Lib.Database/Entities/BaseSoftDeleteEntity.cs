﻿namespace Puzzle.Lib.Database.Entities
{
    public abstract class BaseSoftDeleteEntity : BaseEntity
    {
        /// <summary>
        /// Gets or sets is deleted
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
