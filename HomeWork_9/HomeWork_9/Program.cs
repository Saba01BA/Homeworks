namespace HomeWork_9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Task 1

            //Task 1 Program
            var newCompany = new Company();
            Console.WriteLine("Please Enter Total Salary");
            decimal.TryParse(Console.ReadLine(), out decimal totalSalary);
            newCompany.TotalSalaryGross = totalSalary;
            Console.WriteLine("Is Company based in Georgia? (Y or N)");
            string whereBased = (Console.ReadLine().ToLower().Trim());
            if (whereBased == "y")
            {
                newCompany.IsForeign = false;
            }
            else if (whereBased == "n")
            {
                newCompany.IsForeign = true;
            }

            else
            {
                Console.WriteLine("Invalid Input");
                return;
            }

            newCompany.TaxRate();
        }

            #endregion

        #region Task2




        #endregion

    }

    //ყველა კლასს ამ ფაილში შევქმნი, დავალების შემოწმებაც გამარტივდება.

    #region Task 1 Class

    //Task 1 Class
    public class Company
    {
        public bool IsForeign { get; set; }
        public decimal TotalSalaryGross { get; set; } //ვიცი რომ ერთი პარამეტრი უნდა შემექმნა, უბრალოდ სხვანაირად 
                                                      //არ წარმომიდგენია ამ მეთოდის შექმნა.
        public void TaxRate()
        { 

            if (IsForeign)
            {
                decimal totalTax = TotalSalaryGross / 100 * 18;
                decimal TotalSalaryNett = TotalSalaryGross - totalTax;

                Console.WriteLine($"Total Salary after Tax: {TotalSalaryNett}");

            }

            else
            {
                decimal totalTax = TotalSalaryGross / 100 * 5;
                decimal TotalSalaryNett = TotalSalaryGross - totalTax;

                Console.WriteLine($"Total Salary after Tax: {TotalSalaryNett}");
            }
        }
    #endregion
    }


    //Task 1 Class2
    #region task 1 Class 2 Employee / PayCalculation Method
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte Age { get; set; }
        private string Position { get; set; }
        private int[] WorkedHoursPerDay { get; set; } = new int[7]; //New Syntax

        public decimal PayRate;

        public Employee(string firstName, string lastName, byte age, string position, int[] workedHoursPerDay)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Position = position;
            WorkedHoursPerDay = workedHoursPerDay;

        }


        //თანამშრომლის ხელფასის კალკულაციის მეთოდი
        public void PayCalculation()
        {
            decimal payTotal = 0;
            decimal payDaily = 0;
            if(Position == "Manager" || Position =="manager")
            {
                PayRate = 40;
            } 
            else if (Position == "Developer" || Position == "developer")
            {
                PayRate = 30;
            }
            else if (Position == "Tester" || Position == "tester")
            {
                PayRate = 20;
            }
            else
            {
                PayRate = 10;
            }

            for (int i = 0; i < 7; i++)
            {
                if (i >= 5 && WorkedHoursPerDay[i] > 8)
                {
                    payDaily = 16 * PayRate + ((WorkedHoursPerDay[i] - 8) * (2 * PayRate + 10));
                }
                else if (i >= 5)
                {
                    payDaily = WorkedHoursPerDay[i] * PayRate * 2;
                }
                else if (WorkedHoursPerDay[i] > 8)
                {
                    payDaily = 8 * PayRate + (WorkedHoursPerDay[i] - 8 * (PayRate + 5));
                }
                else
                {
                    payDaily += PayRate * WorkedHoursPerDay[i];
                }

                payTotal += payDaily;
               
            }
            if (WorkedHoursPerDay.Sum() > 50)
            {
                payTotal = payTotal / 100 * 120;
            }

            Console.WriteLine($"Total Pay is {payTotal}");
        }

    #endregion


        



    }

}
