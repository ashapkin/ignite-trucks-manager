using System;
using System.Collections.Generic;
using IgniteTrucksManager.Core.Domain;
using IgniteTrucksManager.Core.Models;
using IgniteTrucksManager.Core.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IgniteTrucksManager.Api.Controllers
{
    /// <summary>
    /// Drivers controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TripsController : ControllerBase
    {
        /** */
        private readonly IRepository<Guid, Trip> _repository;

        /** */
        private readonly TripsManager _tripsManager;

        /** */
        private readonly ILogger<DriversController> _logger;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository.</param>
        /// <param name="tripsManager">Trips manager.</param>
        /// <param name="logger">Logger.</param>
        public TripsController(
            IRepository<Guid, Trip> repository,
            TripsManager tripsManager, 
            ILogger<DriversController> logger)
        {
            _repository = repository;
            _tripsManager = tripsManager;
            _logger = logger;
        }

        /// <summary>
        /// Gets trips.
        /// </summary>
        /// <returns>Trips collection.</returns>
        [HttpGet]
        public IEnumerable<Trip> Get()
        {
            _logger.LogDebug("Getting trips");

            return _repository.GetAll();
        }

        [HttpGet]
        [Route("placeOrder")]
        public object AddTrip(Guid customerId, string from, string to)
        {
            return _tripsManager.PlaceOrder(customerId, from, to);
        }

        [HttpGet]
        [Route("assign")]
        public void AssignDriver(Guid driverId, Guid tripId)
        {
            _tripsManager.AssignDriver(driverId, tripId);
        }

        [HttpGet]
        [Route("finish")]
        public void FinishTrip(Guid tripId, double rating)
        {
            if (rating <= 0 || rating > 5)
            {
                throw new ArgumentException("Rating must be between 0 and 5", nameof(tripId));
            }

            _tripsManager.FinishTrip(tripId, rating);
        }
    }
}