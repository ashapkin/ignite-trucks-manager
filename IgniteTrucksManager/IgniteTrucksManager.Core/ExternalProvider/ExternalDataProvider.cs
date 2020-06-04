using System.Collections.Generic;
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
        private readonly ITrucksRepository _repository;

        public ExternalDataProvider(ITrucksRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Pulls the new data and save it to internal storage.
        /// </summary>
        public void PullNewData()
        {
            IDictionary<Truck, SensorData[]> newData = TrucksData.GetData();

            foreach (KeyValuePair<Truck, SensorData[]> item in newData)
            {
                Truck truck = _repository.Get(item.Key.Id) ?? item.Key;
                truck.AddSensorData(item.Value);

                _repository.Save(truck);
            }
        }
    }
}
