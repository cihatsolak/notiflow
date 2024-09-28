namespace Notiflow.Common.Localize;

[SuppressMessage("Major Code Smell", "S1118:Utility classes should not have public constructors", Justification = "<Pending>")]
public static class FluentVld
{
    public static class Errors
    {
        public const string ID_NUMBER = "1000";
        public const string CUSTOMER_NAME = "1001";
        public const string CUSTOMER_SURNAME = "1002";
        public const string PHONE_NUMBER = "1003";
        public const string EMAIL = "1004";
        public const string BIRTH_DATE = "1005";
        public const string GENDER = "1006";
        public const string MARRIAGE_STATUS = "1007";
        public const string CUSTOMER_ID = "1008";
        public const string OS_VERSION = "1009";
        public const string DEVICE_CODE = "1010";
        public const string DEVICE_TOKEN = "1011";
        public const string CLOUD_MESSAGE_PLATFORM = "1012";
        public const string EMAIL_BODY = "1013";
        public const string EMAIL_SUBJECT = "1014";
        public const string NOTIFICATION_TITLE = "1015";
        public const string NOTIFICATION_MESSAGE = "1016";
        public const string NOTIFICATION_IMAGE_URL = "1017";
        public const string TEXT_MESSAGE = "1018";
        public const string REFRESH_TOKEN = "1019";
        public const string USERNAME = "1020";
        public const string PASSWORD = "1021";
        public const string DATE = "1022";
        public const string TIME = "1023";
        public const string USER_NAME = "1024";
        public const string USER_SURNAME = "1025";
        public const string FILE = "1026";

    };

    public static class Rules
    {
        public const int PASSWORD_MAX_100_LENGTH = 100;
        public const int DEVICE_TOKEN_MAX_100_LENGTH = 100;
        public const int TEXT_MESSAGE_MAX_300_LENGTH = 300;
        public const int CUSTOMER_NAME_MAX_50_LENGTH = 50;
        public const int CUSTOMER_SURNAME_MAX_75_LENGTH = 75;
    }
}
