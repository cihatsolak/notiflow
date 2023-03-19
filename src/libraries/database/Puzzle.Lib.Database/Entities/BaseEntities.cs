using Puzzle.Lib.Database.Interfaces.Entities;

namespace Puzzle.Lib.Database.Entities
{
    public abstract class BaseEntity : IEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public int Id { get; set; }
    }
}
