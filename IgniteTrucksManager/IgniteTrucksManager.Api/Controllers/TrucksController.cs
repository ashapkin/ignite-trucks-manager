using System.Collections.Generic;
using System.Threading.Tasks;
using IgniteTrucksManager.Core.ComputeTasks;
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
        private readonly IIgniteCompute _igniteCompute;

        /** */
        private readonly ILogger<TrucksController> _logger;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository.</param>
        /// <param name="igniteCompute">Ignite compute.</param>
        /// <param name="logger">Logger.</param>
        public TrucksController(ITrucksRepository repository, IIgniteCompute igniteCompute, ILogger<TrucksController> logger)
        {
            _repository = repository;
            _igniteCompute = igniteCompute;
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

        /// <summary>
        /// Gets trucks.
        /// </summary>
        /// <returns>Trucks collection.</returns>
        [HttpGet("analyze/{truckId}")]
        public Task<bool> Analyze(int truckId)
        {
            return _igniteCompute.AnalyzeFuelConsumption(truckId);
        }
    }
}