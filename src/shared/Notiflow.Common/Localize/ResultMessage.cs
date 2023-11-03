namespace Notiflow.Common.Localize;

public record class ResultMessage
{
    public const int GENERAL_ERROR = -1;
    public const int GENERAL_SUCCESS = 0;

    public const int CUSTOMER_NOT_FOUND = 1000;
    public const int CUSTOMER_EXISTS = 1001;
    public const int CUSTOMER_NOT_DELETED = 1002;
    public const int CUSTOMER_BLOCKING_STATUS_EXISTS = 1003;
    public const int CUSTOMER_EMAIL_ADDRESS_SAME = 1004;
    public const int CUSTOMER_PHONE_NUMBER_SAME = 1005;
    public const int DEVICE_EXISTS = 1006;
    public const int DEVICE_NOT_DELETED = 1007;
    public const int DEVICE_NOT_FOUND = 1008;
    public const int TENANT_NOT_FOUND = 1009;
    public const int CUSTOMERS_EMAIL_ADDRESSES_NOT_FOUND = 1009;
    public const int THE_NUMBER_EMAIL_ADDRESSES_NOT_EQUAL = 1010;
    public const int EMAIL_SENDING_FAILED = 1011;
    public const int NOTIFICATION_SENDING_FAILED = 1012;
    public const int CUSTOMERS_PHONE_NUMBERS_NOT_FOUND = 1013;
    public const int THE_NUMBER_PHONE_NUMBERS_NOT_EQUAL = 1014;
    public const int TEXT_MESSAGE_SENDING_FAILED = 1015;
    public const int SUPPORTED_LANGUAGES_NOT_FOUND = 1016;
    public const int TEXT_MESSAGE_NOT_FOUND = 1017;
    public const int NOTIFICATION_NOT_FOUND = 1018;
    public const int EMAIL_HISTORY_NOT_FOUND = 1019;
    public const int TEXT_MESSAGE_SENDING_ACCEPTED = 1020;
    public const int NOTIFICATION_SENDING_ACCEPTED = 1021;
    public const int EMAIL_SENDING_ACCEPTED = 1022;

    public const int USER_NOT_FOUND = 1023;
    public const int ACCESS_TOKEN_NOT_PRODUCED = 1024;
    public const int REFRESH_TOKEN_NOT_FOUND = 1025;
    public const int REFRESH_TOKEN_COULD_NOT_BE_DELETED = 1026;
    public const int USER_EXISTS = 1027;
    public const int USER_ADDED = 1028;
    public const int USER_UPTATED = 1029;
    public const int USER_NOT_DELETED = 1030;
    public const int USER_DELETED = 1031;
    public const int USER_PROFILE_PHOTO_NOT_UPDATED = 1032;
    public const int USER_PROFILE_PHOTO_UPDATED = 1033;
    public const int ACCESS_TOKEN_GENERATED = 1034;
    public const int TENANT_PERMISSION_NOT_FOUND = 1035;
    public const int TENANT_PERMISSION_UPDATED = 1036;

    
    public const int CUSTOMER_ADDED = 1037;
    public const int CUSTOMER_DELETED = 1038;
    public const int CUSTOMER_UPDATED = 1039;
    public const int CUSTOMER_BLOCK_STATUS_UPDATED = 1040;
    public const int CUSTOMER_EMAIL_UPDATED = 1041;
    public const int CUSTOMER_PHONE_NUMBER_UPDATED = 1042;

    public const int DEVICE_ASSOCIATED_CUSTOMER_ADDED = 1043;
    public const int DEVICE_DELETED = 1044;
    public const int DEVICE_UPDATED = 1045;
    public const int DEVICE_TOKEN_UPDATED = 1046;

    public const int EMAIL_SENDING_SUCCESSFUL = 1047;
    public const int NOTIFICATION_SENDING_SUCCESSFUL = 1048;
    public const int TEXT_MESSAGES_SENDING_SUCCESSFUL = 1049;

    public const int TENANT_COULD_NOT_BE_IDENTIFIED = 1050;
}