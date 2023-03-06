namespace Puzzle.Lib.Cache.Constants.Enums
{
    /// <summary>
    /// Absolute expiration for cache
    /// </summary>
    public enum AbsoluteExpiration
    {
        [Description("Bir Dakika")]
        OneMinutes = 1,

        [Description("Beş Dakika")]
        FiveMinutes = 5,

        [Description("Yirmi Dakika")]
        TwentyMinutes = 20,

        [Description("Otuz Dakika")]
        ThirtyMinutes = 30,

        [Description("Kırk Beş Dakika")]
        FortyFiveMinutes = 45,

        [Description("Bir Saat")]
        OneHour = 60,

        [Description("Bir Buçuk Saat")]
        OneAndHalfHours = 90,

        [Description("İki Saat")]
        TwoHour = 120,

        [Description("Dört Saat")]
        FourtyHour = 240,

        [Description("Sekiz Saat")]
        EightHour = 480,

        [Description("Bir gün")]
        OneDay = 1440
    }
}
