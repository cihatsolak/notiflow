namespace Puzzle.Lib.Validation.Infrastructure.Models
{
    /// <summary>
    /// Represents the response model for a validation operation.
    /// This class is internal and sealed, meaning it can only be accessed from within the assembly in which it is defined, and cannot be inherited from.
    /// </summary>
    internal sealed class ValidationResponseModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether the request was successful or not.
        /// </summary>
        public bool Succeeded { get; init; }

        /// <summary>
        /// Gets or sets the data that the response model holds.
        /// </summary>
        public object Data { get; init; }

        /// <summary>
        /// Gets or sets the status code of the response.
        /// </summary>
        public int StatusCode { get; init; }

        /// <summary>
        /// Gets or sets the status message of the response.
        /// </summary>
        public string StatusMessage { get; init; }

        /// <summary>
        /// Gets or sets the errors that occurred during the request.
        /// </summary>
        public IEnumerable<string> Errors { get; init; }
    }
}
