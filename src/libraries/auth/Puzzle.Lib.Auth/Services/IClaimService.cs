namespace Puzzle.Lib.Auth.Services
{
    public interface IClaimService
    {
        /// <summary>
        /// Get email address from claim types
        /// </summary>
        string EmailAddress { get; }

        /// <summary>
        /// Get name from claim types
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Get user id from claim types
        /// </summary>
        int UserId { get; }

        /// <summary>
        ///  Get role from claim types
        /// </summary>
        string Role { get; }

        /// <summary>
        ///  Get roles from claim types
        /// </summary>
        List<string> Roles { get; }

        /// <summary>
        ///  Get jti from claim types
        /// </summary>
        string Jti { get; }

        /// <summary>
        ///  Get audiences from claim types
        /// </summary>
        List<string> Audiences { get; }

        /// <summary>
        ///  Get audience claim types
        /// </summary>
        string Audience { get; }

        /// <summary>
        ///  Get username claim types
        /// </summary>
        string Username { get; }
    }
}
