namespace Aether.Helpers
{
    public static class ValidationUtils
    {
        public static bool IsValidEmailAddress(string src)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(src);
                return (addr.Address == src);
            }
            catch
            {
                return false;
            }
        }
    }
}
