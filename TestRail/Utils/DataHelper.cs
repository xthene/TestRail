namespace TestRail.Utils
{
    public static class DataHelper
    {
        public static string CreateStringByLength(int length)
        {
            Random random = new Random();
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}
