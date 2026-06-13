using System;
using System.Collections.Generic;
using System.Text;

namespace Task__Vehicle_Fleet_System
{
    internal abstract class Vehicle
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double Mileage { get; set; }
        protected Vehicle(string make, string model, int year, double mileage)
        {
            Make = make;
            Model = model;
            Year = year;
            Mileage = mileage;

        }


        public abstract double FuelCalculation(double distanceKm);
        public abstract string PerformMaintenance();
        public abstract string GetVehicleType();
        public virtual string GetReport() 
            {
            return $"[{GetVehicleType()}] {Year} {Make} {Model} | {Mileage:N0} km";
            }
    }
}
