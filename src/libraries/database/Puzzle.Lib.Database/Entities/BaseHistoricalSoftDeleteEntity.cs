﻿namespace Puzzle.Lib.Database.Entities
{
    public abstract class BaseHistoricalSoftDeleteEntity : BaseHistoricalEntity
    {
        /// <summary>
        /// Gets or sets is deleted
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
