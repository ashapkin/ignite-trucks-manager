using System;
using System.Collections.Generic;
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
        private readonly ILogger<DriversController> _logger;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository.</param>
        /// <param name="logger">Logger.</param>
        public TripsController(IRepository<Guid, Trip> repository, ILogger<DriversController> logger)
        {
            _repository = repository;
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
    }
}