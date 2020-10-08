using System;
using System.Collections.Generic;
using IgniteRoute.Core.Models;
using IgniteRoute.Core.Repo;
using Microsoft.AspNetCore.Mvc;

namespace IgniteRoute.Api.Controllers
{
    /// <summary>
    /// Drivers controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DriversController : ControllerBase
    {
        /** */
        private readonly IDriversRepository _repository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository.</param>
        public DriversController(IDriversRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets drivers.
        /// </summary>
        /// <returns>Drivers collection.</returns>
        [HttpGet]
        [Route("add")]
        public Driver Add(string name)
        {
            var driver = new Driver {Name = name, Id = Guid.NewGuid()};
            _repository.Save(driver);
            return driver;
        }

        /// <summary>
        /// Gets drivers.
        /// </summary>
        /// <returns>Drivers collection.</returns>
        [HttpGet]
        public IEnumerable<Driver> Get()
        {
            return _repository.GetAll();
        }
    }
}