using System;
using System.Collections.Generic;
using System.Text;

namespace Task__Vehicle_Fleet_System
{
    internal class FleetManager
    {
        private List<Vehicle> _fleet = new();

        public void AddVehicle(Vehicle v) 
        {
            _fleet.Add(v);
        }
        public void PrintAllReports() 
        {
            foreach (Vehicle v  in _fleet)
            {
                Console.WriteLine(v.GetReport());
            }
        }
        public double TotalTripCost(double distanceKm)
        {
            double totalTripCost = 0;
            foreach (Vehicle v in _fleet)
            {
                totalTripCost += v.FuelCalculation(distanceKm);
            }
            return totalTripCost;
        }

        public Vehicle MostExpensiveForTrip(double distanceKm)
        {
            Vehicle mostExpensive = _fleet[0];
            foreach (Vehicle v in _fleet)
            {
                if (v.FuelCalculation(distanceKm) > mostExpensive.FuelCalculation(distanceKm))
                {
                    mostExpensive = v;
                }
            }
            return mostExpensive;
        }

            public void PrintMaintenanceDue()
        {
            foreach (IMaintainable m in _fleet.OfType<IMaintainable>())
            {
                if (m.NeedsMaintenance())
                    Console.WriteLine(m.MaintenanceInfo());
            }
        }

    }
    }
    

