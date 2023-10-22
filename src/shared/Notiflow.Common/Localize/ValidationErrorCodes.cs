namespace Notiflow.Common.Localize;

public record class ValidationErrorCodes
{
    public const int GENERAL_ERROR = -1;

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


    public const int USER_NOT_FOUND = 2000;
    public const int ACCESS_TOKEN_NOT_PRODUCED = 2001;
    public const int REFRESH_TOKEN_NOT_FOUND = 2002;
    public const int REFRESH_TOKEN_COULD_NOT_BE_DELETED = 2003;
    public const int USER_EXISTS = 2004;
    public const int USER_ADDED = 2005;
    public const int USER_UPTATED = 2006;
    public const int USER_NOT_DELETED = 2007;
    public const int USER_DELETED = 2008;
    public const int USER_PROFILE_PHOTO_NOT_UPDATED = 2009;
    public const int USER_PROFILE_PHOTO_UPDATED = 2010;
    public const int ACCESS_TOKEN_GENERATED = 2011;
    public const int TENANT_PERMISSION_NOT_FOUND = 2012;
    public const int TENANT_PERMISSION_UPDATED = 2013;

    public const int GENERAL_SUCCESS = 8000;
    public const int CUSTOMER_ADDED = 8001;
    public const int CUSTOMER_DELETED = 8002;
    public const int CUSTOMER_UPDATED = 8003;
    public const int CUSTOMER_BLOCK_STATUS_UPDATED = 8004;
    public const int CUSTOMER_EMAIL_UPDATED = 8005;
    public const int CUSTOMER_PHONE_NUMBER_UPDATED = 8006;

    public const int DEVICE_ASSOCIATED_CUSTOMER_ADDED = 8007;
    public const int DEVICE_DELETED = 8008;
    public const int DEVICE_UPDATED = 8009;
    public const int DEVICE_TOKEN_UPDATED = 8010;

    public const int EMAIL_SENDING_SUCCESSFUL = 8011;
    public const int NOTIFICATION_SENDING_SUCCESSFUL = 8012;
    public const int TEXT_MESSAGES_SENDING_SUCCESSFUL = 8013;
}