using System;
using System.Collections.Generic;
using System.Text;

namespace Task__Vehicle_Fleet_System
{
    internal class Truck : Vehicle, IMaintainable
    {
        public double LoadTons { get; private set; } 
        public double FuelPer100Kms { get; private set; }
        public Truck(string make, string model, int year, double mileage, double loadTons, double fuelPer100Kms) : base(make, model, year, mileage)
        {
            LoadTons = loadTons;
            FuelPer100Kms = fuelPer100Kms;
        }

        public override double FuelCalculation(double distanceKm)
        {
            return (FuelPer100Kms + LoadTons * 2) / 100 * distanceKm * 1.85; 
        }

        public override string GetReport()
        {
            return $"[{GetVehicleType()}] {Year} {Make} {Model} | {Mileage:N0} km | Load in Tons {LoadTons}";  
        }

        public override string GetVehicleType()
        {
            return "Truck";
        }

        public override string PerformMaintenance()
        {
           return "Maintenance Launched: Change Oil | Check Wheels";
        }

        public bool NeedsMaintenance()
        {
            return Mileage > 25000;
        }

        public string MaintenanceInfo()
        {
            return NeedsMaintenance()
            ? $"{Make} {Model} — service due"
           : $"{Make} {Model} — OK";
        }
    }
}
