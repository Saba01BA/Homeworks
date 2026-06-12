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
            if (!bank1.CheckUserHistory())

                //5 თვე 1000 ლარი
                Console.WriteLine($"\nTotal Pay: {bank1.CalculateLoanPercent(5, 1000) + 1000}" +
                        $"\nTotal Percent:{bank1.CalculateLoanPercent(5, 1000)}");
            else Console.WriteLine("sorry your History is too bad bro");
            
            #endregion
        }
    }
}
