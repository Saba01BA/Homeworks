using Task__Vehicle_Fleet_System;

internal class ElectricCar : Vehicle
{
    public double BatteryAgeYears { get; private set; }

    public ElectricCar(string make, string model, int year, double mileage,
                       double batteryAgeYears)
        : base(make, model, year, mileage)
    {
        BatteryAgeYears = batteryAgeYears;
       
    }

    public override double FuelCalculation(double distanceKm)
        => distanceKm * 0.32;

    public override string GetVehicleType() => "Electric Car";

    public override string PerformMaintenance()
    {
        string batteryStatus = BatteryAgeYears > 5
            ? "Battery replacement recommended"
            : "Battery OK";
        return $"Electric maintenance: {batteryStatus} | Check OS update";
    }
}