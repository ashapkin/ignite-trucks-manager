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
    public class DriversController : ControllerBase
    {
        /** */
        private readonly IRepository<Guid, Driver> _repository;

        /** */
        private readonly ILogger<DriversController> _logger;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository.</param>
        /// <param name="logger">Logger.</param>
        public DriversController(IRepository<Guid, Driver> repository, ILogger<DriversController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Gets drivers.
        /// </summary>
        /// <returns>Drivers collection.</returns>
        [HttpGet]
        public IEnumerable<Driver> Get()
        {
            _logger.LogDebug("Getting drivers");

            return _repository.GetAll();
        }

       
        /// <summary>
        /// Queries drivers.
        /// </summary>
        /// <param name="sql">SQL statement.</param>
        /// <returns>Query result</returns>
        [HttpGet]
        [Route("query")]
        public object Query(string sql)
        {
            _logger.LogDebug("Querying drivers");

            return _repository.Query(sql);
        }

        [HttpGet]
        [Route("topdrivers")]
        public object TopDriversSql()
        {
            _logger.LogDebug("Getting top drivers");

            var sql = "select Name, Rating from Driver order by Rating desc";

            return _repository.Query(sql);
        }
    }
}