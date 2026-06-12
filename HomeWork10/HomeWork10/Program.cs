namespace HomeWork10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Task 1

           var worker = new Class1();
            worker.Storage = 128;
            worker.Write();
            worker.Read();
            worker.Edit();
            worker.Delete();

            #endregion

            #region Task 2
            
            IFinanceOperations bank1 = new Bank();
            //5 თვე 1000 ლარი
            if (!bank1.CheckUserHistory()) 
                Console.WriteLine($"\nTotal Percent:{bank1.CalculateLoanPercent(5, 1000)}");

            else 
                Console.WriteLine("sorry your History is too bad bro");

            IFinanceOperations microFinance = new MicroFinance();
            Console.WriteLine($"Your Total Payment is {microFinance.CalculateLoanPercent(5, 1000)} ");

            
            #endregion
        }
    }
}
