using IgniteTrucksManager.Core.Models;
using IgniteTrucksManager.Core.Repo;

namespace IgniteTrucksManager.Core.ExternalProvider
{
    /// <summary>
    /// An interface that pulls data from an external source and
    /// saves it within the Ignite.
    /// </summary>
    public class ExternalDataProvider
    {
        /** */
        private readonly IDriversRepository _repository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository.</param>
        public ExternalDataProvider(IDriversRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Pulls the new data and save it to internal storage.
        /// </summary>
        public void PullNewData()
        {
            foreach (Driver driver in DriversData.GetDrivers())
            {
                _repository.Save(driver);
            }
        }
    }
}
