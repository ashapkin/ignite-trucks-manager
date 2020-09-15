using System;
using System.Linq;
using IgniteTrucksManager.Core.Models;
using IgniteTrucksManager.Core.Repo;

namespace IgniteTrucksManager.Core.Domain
{
    /// <summary>
    /// Trips manager.
    /// </summary>
    public class TripsManager
    {
        /** */
        private static readonly Random Rnd = new Random(17);
        /** */
        private readonly IRepository<Guid, Customer> _customersRepository;
        /** */
        private readonly IRepository<Guid, Trip> _tripsRepository;
        /** */
        private readonly IRepository<Guid, Driver> _driversRepository;
        public TripsManager(IRepository<Guid, Customer> customersRepository,
            IRepository<Guid, Trip> tripsRepository,
            IRepository<Guid, Driver> driversRepository)
        {
            _customersRepository = customersRepository ??
                                   throw new ArgumentNullException(nameof(customersRepository));
            _tripsRepository = tripsRepository ??
                               throw new ArgumentNullException(nameof(tripsRepository));
            _driversRepository = driversRepository ??
                                 throw new ArgumentNullException(nameof(driversRepository));
        }

        /// <summary>
        /// Place a new order.
        /// </summary>
        /// <param name="customerId">Customer id.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public Trip PlaceOrder(Guid customerId, string from, string to)
        {
            var res = _customersRepository.Query("select Balance from Customer where Id=?", customerId)?.FirstOrDefault();
            if (res == null)
            {
                throw new ArgumentException("CustomerId was not found");
            }

            var currentAmount = decimal.ToInt32((decimal) res);
            if (currentAmount <= 0)
            {
                throw new ApplicationException("Customer's balance is empty");
            }

            //create random price
            var price = Rnd.Next(1, currentAmount);

            var trip = new Trip
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                From = from,
                To = to,
                Price = price,
                RequestDateTime = DateTime.UtcNow,
            };

            _tripsRepository.Save(trip);

            return trip;
        }

        /// <summary>
        /// Assign a driver to an order.
        /// </summary>
        /// <param name="driverId">Driver id.</param>
        /// <param name="tripId">Trip id.</param>
        public void AssignDriver(Guid driverId, Guid tripId)
        {
            _tripsRepository.Query("update Trip set driverId=? where id=?", driverId, tripId);
        }

        /// <summary>
        /// Finishing a trip.
        /// </summary>
        /// <param name="tripId">Trip id.</param>
        /// <param name="rating">Rating.</param>
        public Trip FinishTrip(Guid tripId, double rating)
        {
            //Note, that this method is not optimized and it's better to replace it with a compute call, 
            //also our data is not collocated thus there might be some additional network hops.

            //The method is not transactional as well.
            
            var trip = _tripsRepository.Get(tripId);
            _customersRepository.Query("update customer set balance = balance - ? where id=?", 
                trip.Price,
                trip.CustomerId);

            //add rating recalculation
            _driversRepository.Query("update driver set balance = balance + ? where id=?", trip.Price, trip.DriverId);

            trip.FinishDateTime = DateTime.UtcNow;
            _tripsRepository.Save(trip);

            return trip;
        }
    }
}
