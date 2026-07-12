using System.IO.Compression;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text.Json;

namespace DesignPatterns
{
    #region Task 1 
    public interface IChair
    {
        public void CheckChair();  
    }
    public interface ISofa
    {
        public void CheckSofa();
    }
    public interface ITable
    {
        public void CheckTable();
    }


    public class ModernChair : IChair
    {
        public void CheckChair()
        {
            Console.WriteLine("Its a Modern Chair");
        }
    }
    public class VictorianChair : IChair
    {
        public void CheckChair()
        {
            Console.WriteLine("Its a Victorian Chair");
        }
    }
    public class ArtDecoChair : IChair
    {
        public void CheckChair()
        {
            Console.WriteLine("Its an ArtDecoChair");
        }
    }

    public class ModernTable : ITable
    {
        public void CheckTable()
        {
            Console.WriteLine("Its a Modern Table");
        }
    }
    public class VictorianTable : ITable
    {
        public void CheckTable()
        {
            Console.WriteLine("Its a Victorian Table");
        }
    }
    public class ArtDecoTable : ITable
    {
        public void CheckTable()
        {
            Console.WriteLine("Its an ArtDeco Table");
        }
    }


    public class ModernSofa : ISofa
    {
        public void CheckSofa()
        {
            Console.WriteLine("Its a Modern Sofa");
        }
    }

    public class VictorianSofa : ISofa
    {
        public void CheckSofa()
        {
            Console.WriteLine("Its a Victorian Sofa");
        }
    }
    public class ArtDecoSofa : ISofa
    {
        public void CheckSofa()
        {
            Console.WriteLine("Its an ArtDeco Sofa");
        }
    }

    public interface IAbstractFactory
    {
        IChair CreateChair();
        ISofa CreateSofa();
        ITable CreateTable();
    }

    public class ModernFurnitureFactory : IAbstractFactory
    {
        public IChair CreateChair() => new ModernChair();


        public ISofa CreateSofa() => new ModernSofa();


        public ITable CreateTable() => new ModernTable();

    }

    public class ArtDecoFurnitureFactory : IAbstractFactory
    {
        public IChair CreateChair() => new ArtDecoChair();


        public ISofa CreateSofa() => new ArtDecoSofa();


        public ITable CreateTable() => new ArtDecoTable();

    }

    public class VictorianFurnitureFactory : IAbstractFactory
    {
        public IChair CreateChair() => new VictorianChair();


        public ISofa CreateSofa() => new VictorianSofa();


        public ITable CreateTable() => new VictorianTable();

    }

    public class OrderManagement
    {
        private readonly IChair _chair;
        private readonly ITable _table;
        private readonly ISofa _sofa;
        public OrderManagement(IAbstractFactory factory)
        {
            _chair = factory.CreateChair();
            _sofa = factory.CreateSofa();
            _table = factory.CreateTable();
        }
        public void CheckFurniture() 
        {
            Console.WriteLine("=====================");
            _chair.CheckChair();
            Console.WriteLine("=====================");
            _sofa.CheckSofa();
            Console.WriteLine("=====================");
            _table.CheckTable();
            Console.WriteLine("=====================");

        }
    }

    #endregion

    #region Task 2

    public interface IAct
    {
        void Act();
        
    }
    public class MainActor() : IAct
    {
        public void Act()
        {
            Console.WriteLine("Main Actor is Acting in a Normal Scene");
        }
    }
    public class StuntDouble() : IAct
    {
        public void Act() => ActDangerous();
     
        public void ActDangerous()
        {
            Console.WriteLine("The Stunt Double is acting in a Dangerous Scene");
        }
    }

    public class ActorProxy : IAct
    {
        private readonly MainActor _mainActor;
        private readonly StuntDouble _stuntDouble;
        private readonly bool _isDangerous;
       
        public ActorProxy(MainActor mainActor,StuntDouble stuntDouble, bool isDangerous)
        {
            _mainActor = mainActor;
            _stuntDouble = stuntDouble;
            _isDangerous = isDangerous;
        }
        public void Act()
        {
            if (_isDangerous)
            {
                _stuntDouble.Act();
            }
            else
            {
                _mainActor.Act();
            }
        }
    }

    #endregion

    #region Task 3

    public class Header
    {
        public string Content { get; set; } = string.Empty;
        public Header(string content)
        {
            Content = content;
        }
        public string PDFRender()
        {
            var PDFRendered = $"Header: {Content}";
                return PDFRendered;
        }
        public string HTMLRender()
        {
            var HTMLrendered = $"<header>{Content}</header>";
            return HTMLrendered;
        }
    }
    public class Body
    {
        public string Content { get; set; }
        public Body(string content)
        {
            Content = content;
        }
        public string PDFRender()
        {
            var PDFRendered = $"Body:\n {Content}";
            return PDFRendered;
        }
        public string HTMLRender()
        {
            var HTMLrendered = $"<body>\n{Content}\n</body>";
            return HTMLrendered;
        }
    }
    public class Footer
    {
        public string Content { get; set; }
        public Footer(string content)
        {
            Content = content;
        }
        public string PDFRender()
        {
            var PDFRendered = $"Footer: {Content}";
            return PDFRendered;
        }
        public string HTMLRender()
        {
            var HTMLrendered = $"<footer>{Content}</footer>";
            return HTMLrendered;
        }
    }

    public class ReportGenerator
    {
        private readonly Header _header;
        private readonly Body _body;
        private readonly Footer _footer;
        public ReportGenerator(Header header, Body body, Footer footer)
        {
            _header = header;
            _body = body;
            _footer = footer;
        }
         public string PDFGenerate()
        {
            return $"{_header.PDFRender()}\n{_body.PDFRender()}\n{_footer.PDFRender()}";
        }  
        
        public string HTMLGenerate()
        {
            return $"{_header.HTMLRender()}\n{_body.HTMLRender()}\n{_footer.HTMLRender()}";
        }

    }

    #endregion

    #region Task 4

    public interface IFileAction
    {
        void Execute(string filePath);
    }
    public class TxtAction : IFileAction
    {
        public void Execute(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File doesnt Exist");
                return;
            }
            File.Delete(filePath);
           
        }
    }
    public class JsonAction : IFileAction
    {
        public void Execute(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File doesntExist");
                return;
            }
            var jsonString = File.ReadAllText(filePath);
            Console.WriteLine(jsonString);
        }
    }
    public class ZipAction : IFileAction
    {
        public void Execute(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File doesnt Exist");
                return;
            }
            var directory = Path.GetDirectoryName(filePath) ?? "";
            var backUpPath = Path.Combine(directory, "backup");

            Directory.CreateDirectory(backUpPath);
            ZipFile.ExtractToDirectory(filePath, backUpPath);
        }
    }

    public class DictionaryHold:IFileAction
    {
        private readonly Dictionary<string, IFileAction> _actions;

        public DictionaryHold(ZipAction zipAction, TxtAction txtAction, JsonAction jsonAction)
        {
            _actions = new Dictionary<string, IFileAction>
    {
        { ".zip", zipAction },
        { ".txt", txtAction },
        { ".json", jsonAction }
    };
        }

        public void Execute(string filePath)
        {
            var extension = Path.GetExtension(filePath);

            if (_actions.TryGetValue(extension, out var action))
            {
                action.Execute(filePath);
            }
            else
            {
                Console.WriteLine("Unsupported file extension.");
            }
        }
    }
    

    #endregion
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Task 4 (vol 2)

            Console.WriteLine("Enter the full path of the file to process:");
            var filePath = Console.ReadLine() ?? "";

            var zipAction = new ZipAction();
            var txtAction = new TxtAction();
            var jsonAction = new JsonAction();

            var dispatcher = new DictionaryHold(zipAction, txtAction, jsonAction);
            dispatcher.Execute(filePath);

            #endregion

            #region task 3 (vol 2)

            Console.WriteLine("Welcome to the PDF/HTML Generator");
            Console.WriteLine("=================================");
            Console.WriteLine("Enter Header:");
            var header =  Console.ReadLine()?? "";
            Console.Clear();
            Console.WriteLine("Enter Body: ");
            var body = Console.ReadLine() ?? "";
            Console.Clear();
            Console.WriteLine("Enter Footer: ");
            var footer = Console.ReadLine() ?? "";
            Console.Clear();
            Console.WriteLine("PDF or HTML (1/2)");
            var userChoice = Console.ReadLine()?? "";
            Console.Clear();
            var reportGenerator = new ReportGenerator(
                new Header(header),
                new Body(body),
                new Footer(footer));
            if (userChoice == "1")
            {
                Console.WriteLine(reportGenerator.PDFGenerate());
            }
            else if (userChoice == "2")
            {
                Console.WriteLine(reportGenerator.HTMLGenerate());
            }
            else
            {
                Console.WriteLine("Invalid Option, Exiting");
                return;
            }
            #endregion

            #region task 2 (vol 2)
            var mainActor = new MainActor();
            var stuntDouble = new StuntDouble();
            bool isDangerous;
            Console.WriteLine("Hello Director, is the scene Dangerous? (Y/N)");
            var answer = (Console.ReadLine() ?? "").ToLower().Trim();
            
            if (answer == "y")
            {
                isDangerous = true;
            }
           else  if (answer == "n")
            {
                isDangerous = false;
            }
            else
            {
                Console.WriteLine("Wrong answer, Exiting");
                return;
            }

            var actorProxy = new ActorProxy(mainActor, stuntDouble, isDangerous);
            actorProxy.Act();
            #endregion

            #region Task 1 (vol 2)
            var victorianFactory = new VictorianFurnitureFactory();
            var modernFactory = new ModernFurnitureFactory();
            var artDecoFactory = new ArtDecoFurnitureFactory();
            Console.WriteLine("Choose a Style of your Furniture");
            Console.WriteLine("1. Victorian\n2.Modern\n3.ArtDeco\n");
            Console.WriteLine("YOUR CHOICE:"); 
            var choice =Console.ReadLine()?? "";
            OrderManagement? order = null;
            
            switch (choice)
            {
                case "1":
                    order = new OrderManagement(victorianFactory);
                    break;
                case "2":
                    order = new OrderManagement(modernFactory);
                    break;

                case "3":
                    order = new OrderManagement(artDecoFactory);
                    break;


                default:
                    Console.WriteLine("Please choose an Existing Style");
                    break;
            }

            if (order is null)
            {
                Console.WriteLine("No valid furniture style was selected. Exiting.");
                return;
            }

            Console.WriteLine("Order is Ready, Wanna Check? (Y/N)");
            var choice1 = Console.ReadLine() ?? "";
            switch (choice1.ToLower().Trim())
            {
                case "y":
                    order.CheckFurniture();
                    return;
                case "n":
                    Console.WriteLine("okay, bye");
                    Environment.Exit(0);
                    return;

                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }

#endregion



        }
    }
}
