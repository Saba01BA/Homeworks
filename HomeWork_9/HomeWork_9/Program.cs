namespace HomeWork_9
{
    internal class Program
    {
        static void Main(string[] args)
        {
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

    }

    //ყველა კლასს ამ ფაილში შევქმნი, დავალების შემოწმებაც გამარტივდება.

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

    }
}
