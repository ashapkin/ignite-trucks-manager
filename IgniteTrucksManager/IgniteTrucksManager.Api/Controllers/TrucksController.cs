using System.Collections.Generic;
using IgniteTrucksManager.Core.Models;
using IgniteTrucksManager.Core.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IgniteTrucksManager.Api.Controllers
{
    /// <summary>
    /// Trucks controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TrucksController : ControllerBase
    {
        /** */
        private readonly ITrucksRepository _repository;

        /** */
        private readonly ILogger<TrucksController> _logger;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository.</param>
        /// <param name="logger">Logger.</param>
        public TrucksController(ITrucksRepository repository, ILogger<TrucksController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Gets trucks.
        /// </summary>
        /// <returns>Trucks collection.</returns>
        [HttpGet]
        public IEnumerable<Truck> Get()
        {
            return _repository.GetAll();
        }
    }
}