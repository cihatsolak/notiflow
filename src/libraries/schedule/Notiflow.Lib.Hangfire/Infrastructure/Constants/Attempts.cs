namespace Notiflow.Lib.Hangfire.Infrastructure.Constants
{
    /// <summary>
    /// How many times will the job be tried when it fails?
    /// </summary>
    public static class Attempts
    {
        /// <summary>
        /// Try it one time
        /// </summary>
        public const int TryOne = 1;

        /// <summary>
        /// Try it twice
        /// </summary>
        public const int TryTwice = 2;

        /// <summary>
        /// Try it three times
        /// </summary>
        public const int TryThreeTimes = 3;

        /// <summary>
        /// Try it four times
        /// </summary>
        public const int TryFourTimes = 4;

        /// <summary>
        /// Try it five times
        /// </summary>
        public const int TryFiveTimes = 5;
    }
}
