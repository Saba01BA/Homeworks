namespace Contact_Book
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataService dataService = new DataService();
            ContactService contactService = new ContactService(dataService);
           

            while (true)
            {
               
                contactService.ShowMenu();
            }
        }
    }
}
