using System;
using System.Collections.Generic;
using System.Text;

namespace Task__Vehicle_Fleet_System
{
    internal class Car : Vehicle, IMaintainable
    {
        public int TiresAgeYears { get; private set; }
       

        public Car(string make, string model, int year, double mileage, int tiresAgeYears) : base(make, model, year, mileage)
        {
            TiresAgeYears = tiresAgeYears;
        }

        public override double FuelCalculation(double distanceKm)
            => distanceKm*1.25;



        public override string GetVehicleType()
        => "Car";

        public override string PerformMaintenance()
        {
            string tiresCheck = TiresAgeYears > 5
                ? "Tires need to be Changed"
                : "Tires are Fine";
            return $"Maintenance Check: Check Engine: Engine is fine | Check Tires: {tiresCheck}";
        }

        public bool NeedsMaintenance()
        {
            return Mileage > 15000;
        }

        public string MaintenanceInfo()
        {
            return NeedsMaintenance()
           ? $"{Make} {Model} — service due"
           : $"{Make} {Model} — OK";
        }
    }
}
