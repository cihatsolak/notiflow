namespace Puzzle.Lib.Cache.Infrastructure.Constants.Enums
{
    /// <summary>
    /// Specifies the duration for which an item should be cached in minutes.
    /// </summary>
    public enum CacheDuration
    {
        /// <summary>
        /// Cache item for 1 minute.
        /// </summary>
        OneMinute = 1,

        /// <summary>
        /// Cache item for 2 minutes.
        /// </summary>
        TwoMinutes = 2,

        /// <summary>
        /// Cache item for 5 minutes.
        /// </summary>
        FiveMinutes = 5,

        /// <summary>
        /// Cache item for 10 minutes.
        /// </summary>
        TenMinutes = 10,

        /// <summary>
        /// Cache item for 15 minutes.
        /// </summary>
        FifteenMinutes = 15,

        /// <summary>
        /// Cache item for 30 minutes.
        /// </summary>
        ThirtyMinutes = 30,

        /// <summary>
        /// Cache item for 1 hour (60 minutes).
        /// </summary>
        OneHour = 60,

        /// <summary>
        /// Cache item for 2 hours (120 minutes).
        /// </summary>
        TwoHours = 120,

        /// <summary>
        /// Cache item for 6 hours (360 minutes).
        /// </summary>
        SixHours = 360,

        /// <summary>
        /// Cache item for 1 day (1440 minutes).
        /// </summary>
        OneDay = 1440
    }
}
