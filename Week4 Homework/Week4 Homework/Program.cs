Console.BackgroundColor = ConsoleColor.Blue;
Console.WriteLine("My Name is Saba Sauri \nWhat is your Name?");

string? userInput = Console.ReadLine();
Console.WriteLine($"Its nice meeting you {userInput}!");
Console.ReadKey();