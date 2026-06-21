namespace ToDo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataService dataService = new DataService();
            LoggerService logger = new LoggerService();
            TaskService taskService = new TaskService(dataService, logger);
            while (true)
            {
                taskService.ShowMenu();
            }
        }
    }
}
