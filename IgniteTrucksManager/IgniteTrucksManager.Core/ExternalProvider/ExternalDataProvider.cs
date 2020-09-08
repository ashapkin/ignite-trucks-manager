using System;
using System.Collections.Generic;
using IgniteTrucksManager.Core.Models;
using IgniteTrucksManager.Core.Repo;

namespace IgniteTrucksManager.Core.ExternalProvider
{
    /// <summary>
    /// An interface that pulls data from an external source and
    /// saves it within the Ignite.
    /// </summary>
    public class ExternalDataProvider
    {
        /** Trucks repository. */
        private readonly IRepository<int, Truck> _trucksRepository;

        /** Drivers repository. */
        private readonly IRepository<Guid, Driver> _driversRepository;

        /** Customers repository. */
        private readonly IRepository<Guid, Customer> _customersRepository;

        /** Trips repository. */
        private readonly IRepository<Guid, Trip> _tripsRepository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="trucksRepository">Trucks repository.</param>
        /// <param name="driversRepository">Drivers repository.</param>
        /// <param name="customersRepository">Customers repository.</param>
        /// <param name="tripsRepository">Trips repository.</param>
        public ExternalDataProvider(IRepository<int, Truck> trucksRepository,
            IRepository<Guid, Driver> driversRepository,
            IRepository<Guid, Customer> customersRepository,
            IRepository<Guid, Trip> tripsRepository)
        {
            _trucksRepository = trucksRepository;
            _driversRepository = driversRepository;
            _customersRepository = customersRepository;
            _tripsRepository = tripsRepository;
        }

        /// <summary>
        /// Pulls the new data and save it to internal storage.
        /// </summary>
        public void PullNewData()
        {
            IDictionary<Truck, SensorData[]> newData = TrucksData.GetData();

            foreach (KeyValuePair<Truck, SensorData[]> item in newData)
            {
                Truck truck = _trucksRepository.Get(item.Key.Id) ?? item.Key;
                truck.AddSensorData(item.Value);

                _trucksRepository.Save(truck);
            }

            foreach (Driver driver in DriversData.GetDrivers())
            {
                _driversRepository.Save(driver);
            }

            foreach (Customer customer in CustomersData.GetCustomers())
            {
                _customersRepository.Save(customer);
            }

            foreach (Trip trip in TripsData.GetTrips())
            {
                _tripsRepository.Save(trip);
            }
        }
    }
}
