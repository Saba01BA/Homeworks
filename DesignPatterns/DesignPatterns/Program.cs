using System.Security.Cryptography;

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
    internal class Program
    {
        static void Main(string[] args)
        {
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
