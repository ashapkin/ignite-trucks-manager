using IgniteRoute.Core.DataLoader;
using Microsoft.AspNetCore.Mvc;

namespace IgniteRoute.Api.Controllers
{
    /// <summary>
    /// Data loader controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DataLoaderController
    {
        /** */
        private readonly IDataLoader _dataProvider;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="dataLoader">Data loader.</param>
        public DataLoaderController(IDataLoader dataLoader)
        {
            _dataProvider = dataLoader;
        }

        /// <summary>
        /// Loads external data.
        /// </summary>
        [HttpGet]
        public string Get()
        {
            _dataProvider.LoadInitialData();
            return "Completed!";
        }
    }
}
