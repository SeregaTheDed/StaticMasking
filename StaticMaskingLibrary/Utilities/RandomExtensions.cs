using System.Text;

namespace StaticMaskingLibrary.utils
{
    internal static class RandomExtensions
    {
        public static void Shuffle<T>(this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
        public static string GetRandomString(this Random rng)
        {
            int stringLenght = 8;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < stringLenght; i++)
            {
                sb.Append((char)rng.Next('a', 'z' + 1));
            }
            return sb.ToString();
        }
    }
}
