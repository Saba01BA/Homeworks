using System.Threading.Channels;

namespace Week4_Homework2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            #region Task 1
            //Task 1 

            Console.WriteLine("Hello, please enter a whole Number");
            var num1 = Convert.ToInt32(Console.ReadLine());
            if (num1 % 5 == 0) 
            {
                Console.WriteLine("YES");
            }
            else
            {
                Console.WriteLine("NO");
            }
            #endregion

            #region Task 2

            //Task 2 - Calculator

            Console.WriteLine("Hello, please enter a whole Number");

            var num2 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("please enter a second whole Number");
            var num3 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("now choose the action ( + , - , * , / )");
            var action = Console.ReadLine();
            
            if (action == "+")
            {
                Console.WriteLine($"{num2}+{num3} = {num2 + num3}");
            }

            else if (action == "-")
            {
                if (num2 > num3)
                {

                Console.WriteLine($"{num2}-{num3} = {num2 - num3}");
                }
                else 
                {
                Console.WriteLine($"{num3}-{num2} = {num3 - num2}");
                }
            }
            else if (action == "/")
            {
                if (num2 > num3)
                {
                    if (num3 ==0)
                    {
                        Console.WriteLine("Error, you tried dividing by 0");

                    }
                    else
                    {
                    Console.WriteLine($"{num2}/{num3} = {(double)num2 / num3}");

                    }

                }
                else
                {
                    if (num2 == 0)
                    {
                        Console.WriteLine("Error, you tried dividing by 0");

                    }
                    else
                    {
                        Console.WriteLine($"{num3}/{num2} = {(double)num3 / num2}");

                    }

                }
            }
            else if (action == "*") 
            { 
                Console.WriteLine($"{num2}*{num3} = {num2 * num3}");
            }

            else
            {
                Console.WriteLine("invalid input");
            }
            #endregion

            #region Task 3

            Console.WriteLine("Enter X number");
            var x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Y number");
            var y = Convert.ToInt32(Console.ReadLine());

            var z = x;
            x = y;
            y = z;

            Console.WriteLine($"X is {x} and Y is {y}");

            //i had to google it to solve this problem :( 


            #endregion

            #region Task 4

            Console.WriteLine("Enter a Whole number");
            var num4 = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine($"{num4} * {i} = {num4*i}");
            }


            #endregion
            

            #region Task 5

            Console.WriteLine("Hello, enter a whole number");
            var num5 = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i <= num5; i++)
            {
                if (i%2==0 && i >0)
                {
                    Console.WriteLine(i*i);
                }
                
            }

            #endregion
        }
    }
}
