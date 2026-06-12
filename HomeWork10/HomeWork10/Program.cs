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

            #endregion
        }
    }
}
