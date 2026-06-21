using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo
{
    
    public class TaskService
    {
        private readonly DataService _dataService;
        private readonly LoggerService _logger;
        List<Task> tasks = new List <Task>();
        public TaskService(DataService dataService, LoggerService logger)
        {
            _logger = logger;
            _dataService = dataService;
            tasks = _dataService.LoadTasks(); 
        }
        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("== To Do List ==");
            Console.WriteLine("1. Add a Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Complete Task");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("0. Save & Exit");
            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1": AddTask(); return;
                case "2": ViewTask(); return;
                case "3": CompleteTask(); return;
                case "4": DeleteTask(); return;
                case "0":
                   _dataService.SaveData(tasks);
                    Console.Clear(); Console.WriteLine("Good Bye!"); Environment.Exit(0); return;

                default: Console.WriteLine("Invalid Input"); return;
            }


        }
        public void AddTask()
        {
            Console.Clear();

            Console.WriteLine("ADD A TASK");
            Console.WriteLine("\nEnter a Name of Your Task:");
            Task task = new Task();
            task.Name = Console.ReadLine() ?? "";
            tasks.Add(task);
            _logger.LogInfo("Task Added");
            Console.WriteLine("Task Added");
            Console.WriteLine("Press any Key to continue...");
            Console.ReadKey();
            
        }
        public void ViewTask()
        {
            Console.Clear();
            if (tasks.Count == 0)
            {
                Console.WriteLine("You have 0 Tasks");
            }
            else
            {
            Console.WriteLine("== To DO List ==");
            foreach (var item in tasks)
            {
                if (item.isCompleted)
                {
                    Console.WriteLine($"{item.Name} | Completed");
                }
                else
                {
                    Console.WriteLine($"{item.Name} | Incomplete");
                }
            }
            }

            Console.WriteLine("Press any Key to continue...");
            Console.ReadKey();
        }
        public void CompleteTask()
        {
            Console.Clear();
            Console.WriteLine("== Choose a Task to Complete ==");
            int n = 0;

            foreach (var item in tasks)
            {
                if (item.isCompleted)
                {
                    Console.WriteLine($"{n + 1}.{item.Name} | Completed");
                }
                else
                {
                    Console.WriteLine($"{n + 1}.{item.Name} | Incomplete");
                }
                n++;
            }
            int.TryParse(Console.ReadLine(), out n);
            if (tasks[n-1].isCompleted)
            {
                Console.WriteLine("The Task is Already Completed");
                _logger.LogWarning("You cant complete a Task Twice");
            }
            else
            {
                tasks[n-1].isCompleted = true;
                Console.WriteLine("Task Status Changed");
                _logger.LogInfo($"{tasks[n-1].Name} - Task Status Changed");
                Console.Clear();
                n = 0;
                foreach (var item in tasks)
                {
                    if (item.isCompleted)
                    {
                        Console.WriteLine($"{n + 1}.{item.Name} | Completed");
                    }
                    else
                    {
                        Console.WriteLine($"{n + 1}.{item.Name} | Incomplete");
                    }
                    n++;
                }

                Console.WriteLine("Press any Key to Continue...");
                Console.ReadKey();
            }

        }
        public void DeleteTask()
        {
            Console.Clear();
            Console.WriteLine("== Choose a Task to Delete ==");
            int n = 0;

            foreach (var item in tasks)
            {
                if (item.isCompleted)
                {
                    Console.WriteLine($"{n + 1}.{item.Name} | Completed");
                }
                else
                {
                    Console.WriteLine($"{n + 1}.{item.Name} | Incomplete");
                }
                n++;
            }
            int.TryParse(Console.ReadLine(), out n);

            _logger.LogInfo($"{tasks[n - 1].Name} - Task Deleted");
            tasks.RemoveAt(n - 1);
            Console.WriteLine("Task Deleted");
            Console.Clear();
            n = 0;
            foreach (var item in tasks)
            {
                if (item.isCompleted)
                {
                    Console.WriteLine($"{n + 1}.{item.Name} | Completed");
                }
                else
                {
                    Console.WriteLine($"{n + 1}.{item.Name} | Incomplete");
                }
                
            }
            Console.WriteLine("Press any Key to Continue...");
            Console.ReadKey();
        }
    }
}
