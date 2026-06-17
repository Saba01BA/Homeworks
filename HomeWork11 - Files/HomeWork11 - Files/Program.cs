using System.Xml;

namespace HomeWork11___Files
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Task1
            if (!File.Exists("Output.txt"))
            {
                Console.WriteLine("How many lines do you want to write in a file?");
                int.TryParse(Console.ReadLine(), out int input);
                string[] lines = new string[input];

                for (int i = 0; i < input; i++)
                {
                    Console.WriteLine($"Line on position {i + 1}");
                    lines[i] = Console.ReadLine();
                }
                File.WriteAllLines($"Output.txt", lines);

            }


            string[] savedLines = File.ReadAllLines("Output.txt");
            Console.WriteLine($"The last Line: {savedLines[savedLines.Length - 1]}");

            #endregion

            #region Task2
            if (!File.Exists("multiplications.txt"))
            {

                Console.WriteLine("Enter a number");
                int.TryParse(Console.ReadLine(), out int n);
                string[] multi = new string[9];
                for (int i = 1; i <= 9; i++)
                {
                    for (int j = 1; j <= n; j++)
                    {
                        if (j != n)
                        {
                            multi[i - 1] += $"{i} * {j} = {i * j} |";
                        }
                        else
                        {
                            multi[i - 1] += $"{i} * {j} = {i * j}";
                        }
                    }
                }
                File.WriteAllLines("multiplications.txt", multi);
            }
            #endregion


            #region task3
            if (!File.Exists("strings.xml"))
            {
                Console.WriteLine("Enter your String");
                string userInput = Console.ReadLine();
                Console.WriteLine("Enter the number");
                int.TryParse(Console.ReadLine(), out int n);
                int chunkLenght = userInput.Length / n;
                string chunk = "";
                using (XmlWriter writer = XmlWriter.Create("strings.xml"))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("XML");
                    for (int i = 0; i < n; i++)
                    {
                        chunk = userInput.Substring(i * chunkLenght, chunkLenght);


                        writer.WriteStartElement(chunk);
                        writer.WriteString($"string{i + 1}");
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }

            }
            #endregion

            #region task 4

            #endregion
        }
    }

    public class BirthdayCalculation
    {
        public string Birthday { get; set; }

        public string CurrentDate { get; set; }

      
    }
}
