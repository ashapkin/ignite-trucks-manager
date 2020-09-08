using System;
using System.Collections.Generic;
using IgniteTrucksManager.Core.Models;
using IgniteTrucksManager.Core.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IgniteTrucksManager.Api.Controllers
{
    /// <summary>
    /// Customers controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        /** */
        private readonly IRepository<Guid, Customer> _repository;

        /** */
        private readonly ILogger<DriversController> _logger;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository.</param>
        /// <param name="logger">Logger.</param>
        public CustomersController(IRepository<Guid, Customer> repository, ILogger<DriversController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Gets customers.
        /// </summary>
        /// <returns>Customers collection.</returns>
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            _logger.LogDebug("Getting customers.");

            return _repository.GetAll();
        }
    }
}