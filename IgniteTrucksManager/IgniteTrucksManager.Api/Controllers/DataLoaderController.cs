using IgniteTrucksManager.Core.ExternalProvider;
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


        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="dataProvider">Data provider.</param>
        /// <param name="logger">Logger.</param>
        public DataLoaderController(ExternalDataProvider dataProvider, ILogger<DataLoaderController> logger)
        {
            _dataProvider = dataProvider;
            _logger = logger;
        }

        /// <summary>
        /// Loads external data.
        /// </summary>
        [HttpGet]
        public string Get()
        {
            _logger.LogDebug("Loading data");

            _dataProvider.PullNewData();
            return "Completed!";
        }
    }
}
