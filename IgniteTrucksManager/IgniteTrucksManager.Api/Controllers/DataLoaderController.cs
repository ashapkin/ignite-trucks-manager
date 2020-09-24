using System;
using IgniteTrucksManager.Core.Domain;
using IgniteTrucksManager.Core.ExternalProvider;
using IgniteTrucksManager.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IgniteTrucksManager.Api.Controllers
{
    /// <summary>
    /// Data loader controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DataLoaderController : ControllerBase
    {
        /** */
        private readonly ExternalDataProvider _dataProvider;

        /** */
        private readonly ILogger<DataLoaderController> _logger;

        /** */
        private readonly TripsManager _tripsManager;


        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="dataProvider">Data provider.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="tripsManager">Trips manager.</param>
        public DataLoaderController(ExternalDataProvider dataProvider, ILogger<DataLoaderController> logger,
            TripsManager tripsManager)
        {
            _dataProvider = dataProvider;
            _logger = logger;
            _tripsManager = tripsManager;
        }

        /// <summary>
        /// Loads external data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            _logger.LogDebug("Loading initial data");

            _dataProvider.PullNewData();
            return "Completed!";
        }

        /// <summary>
        /// Verifies placing and finishing trip logic.
        /// </summary>
        /// <returns>Trip.</returns>
        [HttpGet]
        [Route("verifySql")]
        public Trip VerifySql()
        {
            var customerId = Guid.Parse("a5085a58-e732-4f17-988d-65b05b2a8b6a");
            var driverId = Guid.Parse("5da296f4-b944-4eb4-a4fd-08fc4bc7661d");

            var trip = _tripsManager.PlaceOrder(customerId, "FromPoint", "ToPoint");

            _tripsManager.AssignDriver(driverId, trip.Id);

            trip = _tripsManager.FinishTrip(trip.Id, 2);

            return trip;
        }
    }
}
