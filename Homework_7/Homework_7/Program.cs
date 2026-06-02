using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Homework_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Task 1

            Console.WriteLine("Please enter the Radios of the Circle:");
            int radius_Input = int.Parse(Console.ReadLine());
            var areaSquareBig = 4*radius_Input*radius_Input; //area=2r*r
            var areaSquareSmall = 2*radius_Input*radius_Input; //area=d*d/2
            var areaDifference = areaSquareBig - areaSquareSmall;
            Console.WriteLine($"The difference between the areas is {areaDifference}");


            #endregion

            #region Task 2

            Console.WriteLine("Game Rules: to win the Jackpot you need to get Symbol on every reel." +
                              "\nEnter the number of slot reels (3-7)");
            int size = int.Parse(Console.ReadLine());

                string[] jackpot = new string[size];
                for (int i = 0; i < size; i++)
                {
                    Console.WriteLine($"Enter the Symbol on Reel N{i + 1}");  //Enter the number of slot reels (3-7)
                    jackpot[i] = Console.ReadLine().Trim();

                }

                bool allIdentical = jackpot.All(x => x == jackpot[0]); // || jackpot.Length == 0; ?????
                if (allIdentical)
                {
                    Console.WriteLine("YOU WON JACKPOT!");
                }
                else
                {
                    Console.WriteLine("Sorry, you Lost");
                }

            #endregion

            #region Task 3

            Console.WriteLine("Hello to the Championship Management App! \nEnter how many times your team Won!");
            int win = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter how many Times your Team lost?");
            int lose = int.Parse(Console.ReadLine());
            Console.WriteLine("and how many draws?");
            int draw = int.Parse(Console.ReadLine());

            int totalScore = (win * 3) + draw;

            Console.WriteLine($"Your Team has {totalScore} Score\nHope You enjoyed Results");

            #endregion

            #region Task 4

            int[] dailyHours = new int[7];
            int regularPay = 0;
            int overTimePay = 0;
            int totalPay = regularPay = overTimePay;
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"Enter Worked Hours on Day - {i+1}");
                dailyHours[i] = int.Parse(Console.ReadLine());
                overTimePay = 0;

              

                if (dailyHours[i]<=8)
                {
                    regularPay = dailyHours[i] * 10;
                }
                else
                {
                    regularPay = 8 * 10;
                    overTimePay = (dailyHours[i] - 8) * 15; 
                }

                if (i == 5 || i == 6)
                {
                    regularPay = regularPay * 2;
                    overTimePay = overTimePay * 2;
                }

                totalPay += regularPay + overTimePay;

                Console.WriteLine($"Total Weekly Salary: ${totalPay}");

            }


            #endregion

            #region Task 5

            Console.WriteLine("Hello Giorgi :) , " +
                "\nPlease enter how long have you been training daily for last week");
            int[] trainingHours = new int[7];
            int progressDays = 0;
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"How long did you train on day {i+1}");
                trainingHours[i] = int.Parse(Console.ReadLine());
                if (i>0&& trainingHours[i] > trainingHours[i - 1])
                {
                    progressDays++;
                }
            }

            Console.WriteLine(progressDays);


            #endregion

            #region Task 6

            string[] arrayBeispiel = {"Hello", "World", "Programming", "Communication"};
            Console.WriteLine("Enter Length of the word you need to find");
            int userInputLength = int.Parse(Console.ReadLine());
            var length = arrayBeispiel.Where(x => x.Length==userInputLength);
            if (length.Any()) // .Any() = checks if collection has at least 1 element. ! = if empty
                Console.WriteLine(string.Join(" ", length));
            else
                Console.WriteLine("No Elements Found");

            #endregion
        }
    }
}
