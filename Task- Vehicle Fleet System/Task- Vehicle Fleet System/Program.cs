namespace Task__Vehicle_Fleet_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FleetManager fleet = new FleetManager();

            fleet.AddVehicle(new Car("Toyota", "Camry", 2020, 45000, 3));
            fleet.AddVehicle(new Car("BMW", "X5", 2021, 30000, 1));
            fleet.AddVehicle(new Truck("Volvo", "FH16", 2019, 120000, 12, 30));
            fleet.AddVehicle(new ElectricCar("Tesla", "Model 3", 2022, 20000, 2));

            fleet.PrintAllReports();

            Console.WriteLine($"\nTotal trip cost (500km): {fleet.TotalTripCost(500):F2} GEL");

            Vehicle priciest = fleet.MostExpensiveForTrip(500);
            Console.WriteLine($"Most expensive vehicle for 500km: {priciest.Make} {priciest.Model}");

            fleet.PrintMaintenanceDue();

        }
    }
}
