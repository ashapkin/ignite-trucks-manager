using System;
using System.Collections.Generic;
using System.Diagnostics;
using Apache.Ignite.Core;
using IgniteTrucksManager.Core.Models;
using IgniteTrucksManager.Core.Repo;

namespace IgniteTrucksManager.Core
{
    /// <summary>
    /// Program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        static void Main(string[] args)
        {
            using var ignite = Ignition.Start();
            ITrucksRepository repository = new TrucksRepository(ignite);

            Truck truck = GenerateTruck();
            repository.Save(truck);
                
            Truck persistedTruck = repository.Get(truck.Id);
                
            Console.WriteLine(persistedTruck);
            Debug.Assert(persistedTruck != null);
        }

        /// <summary>
        /// Generates new truck instance.
        /// </summary>
        /// <returns></returns>
        private static Truck GenerateTruck()
        {
            var truck = new Truck("001")
                .AddSensorData(new List<SensorData>()
                {
                    new SensorData
                    {
                        DateTimeUtc = DateTime.UtcNow.AddMinutes(-1),
                        FuelLevel = 100,
                        Speed = 60
                    },
                    new SensorData
                    {
                        DateTimeUtc = DateTime.UtcNow,
                        FuelLevel = 90,
                        Speed = 90
                    }
                });
            
            return truck;
        }
    }
}
