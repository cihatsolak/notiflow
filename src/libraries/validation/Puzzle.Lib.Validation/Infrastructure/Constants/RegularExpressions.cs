namespace Puzzle.Lib.Validation.Infrastructure.Constants
{
    public static class RegularExpressions
    {
        /// <summary>
        /// Draft: loremipsum@hotmail.com
        /// </summary>
        public static Regex Email => new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        /// <summary>
        /// Draft: 02165961514 -- 0216 596 15 14
        /// </summary>
        public static Regex Landline => new(@"^(0)([2348]{1})([0-9]{2})\s?([0-9]{3})\s?([0-9]{2})\s?([0-9]{2})$");

        /// <summary>
        /// At least eight characters, at least one uppercase letter, one lowercase letter, one number, and one special character
        /// </summary>
        public static Regex Password => new(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

        /// <summary>
        /// Draft: 2x633x9299x
        /// </summary>
        public static Regex TcNumber => new(@"^[1-9]{1}[0-9]{9}[02468]{1}");

        /// <summary>
        /// Draft: 506 173 0x 3x -- 5061730x3x9
        /// </summary>
        public static Regex MobilePhone => new(@"^(5)([0-9]{2})\s?([0-9]{3})\s?([0-9]{2})\s?([0-9]{2})$");

        /// <summary>
        /// Draft: 1234567890
        /// </summary>
        public static Regex TaxNumber => new(@"^[0-9]{10}$");

        /// <summary>
        /// Draft: 1111222233334444 -- 1111 2222 3333 4444
        /// </summary>
        public static Regex CreditCard => new(@"^([0-9]{4})\s?([0-9]{4})\s?([0-9]{4})\s?([0-9]{4})$");

        /// <summary>
        /// Draft: 34A2344 -- 36A23415 -- 06BK123 -- 08JK1234 -- 81ABC75
        /// </summary>
        public static Regex VehiclePlate => new(@"^(0[1-9]|[1-7][0-9]|8[01])(([A-Z])(\d{4,5})|([A-Z]{2})(\d{3,4})|([A-Z]{3})(\d{2,3}))$");
    }
}
