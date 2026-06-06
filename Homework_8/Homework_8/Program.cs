using System.Diagnostics.Metrics;

namespace Homework_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
           

        }
        static int RootCounter(int a, int b, int n) //Task 1
        {
            int counter = 0;
            for (int x = 1; Math.Pow(x, n) <=b; x++)
            {
                if(a<=Math.Pow(x,n)&&Math.Pow(x, n)<=b)
                {
                    counter++;
                }
            }
            return counter;
        }

        static int SockCounter(string charString) //Task 2
        {
            int pairs = 0;
            int count = 0;
            Dictionary<char, int> sockDictionary = new Dictionary<char, int>();
            foreach (var c in charString)
            {
                if (sockDictionary.ContainsKey(c)) 
                {
                    sockDictionary[c]++;   
                }
                else
                {
                    count = 1;
                    sockDictionary.Add(c, count);
                }
            }

            foreach (var value in sockDictionary.Values)
            {
                pairs += value / 2;
            }



            return pairs;
        }

        static string CommonSuffix(string strA, string strB) //Task 3 (Hardest so far)
        {
            int i = strA.Length - 1;
            int j = strB.Length - 1;

            while (i >= 0 && j >= 0 && strA[i] == strB[j])
            {
                i--;
                j--;
            }

            return strA.Substring(i + 1);
        }

        static void ProcessList<T>(List<T> list) //Task 4
        {
            if (list is List<int> intList)
            {
                int sum = 0;
                foreach (var item in intList)
                {
                  
                    sum += item;
                   
                }
                Console.WriteLine(sum);
            }
            else if (list is List<string>stringList)
            {
                foreach (var item in stringList)
                {
                    Console.WriteLine(item.ToUpper());  
                }
               
            }
            else if (list is List<bool>boolList)
            {
                Console.WriteLine( boolList[0]);
                Console.WriteLine(boolList[(boolList.Count - 1) / 2]);
                Console.WriteLine(boolList[boolList.Count-1]); //prints the last
            }
        }

        static void PrintDigits(int numss) //Task 5 (Couldnt do it without Claude)
        {
            if (numss == 0) return;
            PrintDigits(numss / 10);
            Console.Write(numss % 10);
        }

        static bool checkDuplicates(int[] nums)
        {
            HashSet<int> checkedNums = new HashSet<int>();
            foreach (var num in nums)
            {
                
                if (!checkedNums.Add(num))
                {
                    return true;
                }
                
            }

            return false;

        }
        
    }
}
