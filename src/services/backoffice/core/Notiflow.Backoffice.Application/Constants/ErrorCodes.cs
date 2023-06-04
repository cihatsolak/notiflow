namespace Notiflow.Backoffice.Application.Constants
{
    internal static class ErrorCodes
    {
        internal const int CUSTOMER_NOT_FOUND = 1000;
        internal const int CUSTOMER_EXISTS = 1001;
        internal const int CUSTOMER_NOT_DELETED = 1002;
        internal const int CUSTOMER_BLOCKING_STATUS_EXISTS = 1003;
        internal const int CUSTOMER_EMAIL_ADDRESS_SAME = 1004;
        internal const int CUSTOMER_PHONE_NUMBER_SAME = 1005;

        internal const int TENANT_NOT_FOUND = 6000;
    }
}
