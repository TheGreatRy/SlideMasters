namespace SlideMasters_BlazorApp.Models
{
    public static class ImageRandomizer
    {
        private static Random _random = new Random();
        public static void Shuffle<T>(ref List<T> list)
        {
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = _random.Next(0, i + 1);
                // Swap list[i] with list[j]
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
    }
}
