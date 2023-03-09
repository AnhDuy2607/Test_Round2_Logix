namespace BE.Utilities
{
    public class Helper
    {
        public static string GeneratePasswordWithSalt(string password)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes($"{password}{ConstantValue.PASSWORD_SALT}");
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);
            }
        }
    }
}