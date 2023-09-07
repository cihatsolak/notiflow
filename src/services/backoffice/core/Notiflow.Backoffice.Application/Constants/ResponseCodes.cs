namespace Notiflow.Backoffice.Application.Constants;

internal record class ResponseCodes
{
    /// <summary>
    /// Contains error codes from 1000 to 7999.
    /// </summary>
    internal struct Error
    {
        internal const int CUSTOMER_NOT_FOUND = 1000;
        internal const int CUSTOMER_EXISTS = 1001;
        internal const int CUSTOMER_NOT_DELETED = 1002;
        internal const int CUSTOMER_BLOCKING_STATUS_EXISTS = 1003;
        internal const int CUSTOMER_EMAIL_ADDRESS_SAME = 1004;
        internal const int CUSTOMER_PHONE_NUMBER_SAME = 1005;
        internal const int DEVICE_EXISTS = 1006;
        internal const int DEVICE_NOT_DELETED = 1007;
        internal const int DEVICE_NOT_FOUND = 1008;
        internal const int TENANT_NOT_FOUND = 1009;
        internal const int CUSTOMERS_EMAIL_ADDRESSES_NOT_FOUND = 1009;
        internal const int THE_NUMBER_EMAIL_ADDRESSES_NOT_EQUAL = 1010;
        internal const int EMAIL_SENDING_FAILED = 1011;
        internal const int NOTIFICATION_SENDING_FAILED = 1012;
        internal const int CUSTOMERS_PHONE_NUMBERS_NOT_FOUND = 1013;
        internal const int THE_NUMBER_PHONE_NUMBERS_NOT_EQUAL = 1014;
        internal const int TEXT_MESSAGE_SENDING_FAILED = 1015;
        internal const int SUPPORTED_LANGUAGES_NOT_FOUND = 1016;
        internal const int TEXT_MESSAGE_NOT_FOUND = 1017;
        internal const int NOTIFICATION_NOT_FOUND = 1018;
        internal const int EMAIL_HISTORY_NOT_FOUND = 1019;
    }

    /// <summary>
    /// Contains successful status codes from 8000 to 9999.
    /// </summary>
    internal struct Success
    {
        internal const int OPERATION_SUCCESSFUL = 8000;
        internal const int CUSTOMER_ADDED = 8001;
        internal const int CUSTOMER_DELETED = 8002;
        internal const int CUSTOMER_UPDATED = 8003;
        internal const int CUSTOMER_BLOCK_STATUS_UPDATED = 8004;
        internal const int CUSTOMER_EMAIL_UPDATED = 8005;
        internal const int CUSTOMER_PHONE_NUMBER_UPDATED = 8006;

        internal const int DEVICE_ASSOCIATED_CUSTOMER_ADDED = 8007;
        internal const int DEVICE_DELETED = 8008;
        internal const int DEVICE_UPDATED = 8009;
        internal const int DEVICE_TOKEN_UPDATED = 8010;

        internal const int EMAIL_SENDING_SUCCESSFUL = 8011;
        internal const int NOTIFICATION_SENDING_SUCCESSFUL = 8012;
        internal const int TEXT_MESSAGES_SENDING_SUCCESSFUL = 8013;
    }
}
