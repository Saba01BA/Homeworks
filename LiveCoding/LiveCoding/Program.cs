using System.Globalization;

namespace LiveCoding
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a String");
            string n1 = Console.ReadLine()?? "";
            string[] n11 = n1.Split(" ");
            Console.WriteLine(n11.Length);


            Console.WriteLine("Enter Word N1");
            char[] word1 = (Console.ReadLine() ?? "").ToCharArray();

            Console.WriteLine("Enter Word N2");
            char[] word2 = (Console.ReadLine() ?? "").ToCharArray();
            Array.Sort(word1);
            Array.Sort(word2);
            string sorted = new string(word1);
            string sorted2 = new string(word2);

            if (sorted == sorted2)
            {
                Console.WriteLine("SAME");
            }
            else
            {
                Console.WriteLine("DIFFERENT");
            }



            Console.WriteLine("Enter Lenght of your array");
            int.TryParse(Console.ReadLine(), out int length);
            int[] nums = new int[length];
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine($"Enter Number on Position {i+1}");
                int.TryParse(Console.ReadLine(), out nums[i]);
            }

            Dictionary<int, int> counts = new Dictionary<int, int>();
            foreach (var item in nums)
            {
                if (counts.TryGetValue(item, out int n))
                {
                    counts[item] = n+1;
                }
                else
                {
                    counts.Add(item, 1);   
                }
            }
            int maxCount = 0;
            int mostFrequent = 0;

            foreach (var item in counts)
            {
              if(item.Value > maxCount)
                {
                    maxCount = item.Value;
                    mostFrequent = item.Key;
                }  
            }

            Console.WriteLine($"Most Frequent number in the Array was {mostFrequent} | {maxCount} Times");
        }
    }
}
