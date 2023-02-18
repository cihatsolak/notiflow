namespace Notiflow.Lib.Validation.Infrastructure.Extensions
{
    internal static class EmailValidateExtension
    {
        static readonly string[] tlds = new string[] { "com", "net", "org", "edu", "gov", "us", "uk", "ca", "au", "fr" };

        internal static bool ValidateTld(string email)
        {
            return tlds.Any(p => p == email.Split('.').Last());
        }
    }
}
