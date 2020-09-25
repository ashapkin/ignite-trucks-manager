using System;
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
            IDriversRepository repository = new DriversRepository(ignite);

            Driver driver = GenerateDriver();
            repository.Save(driver);

            Driver persistedDriver = repository.Get(driver.Id);
                
            Console.WriteLine(persistedDriver);
            Debug.Assert(persistedDriver != null);
        }

        /// <summary>
        /// Generates new driver instance.
        /// </summary>
        /// <returns></returns>
        private static Driver GenerateDriver()
        {
            return new Driver
            {
                Id = Guid.NewGuid(),
                Balance = 100,
                Name = "John",
                Rating = 4.3
            };
        }
    }
}
