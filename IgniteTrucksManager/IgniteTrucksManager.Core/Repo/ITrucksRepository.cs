using System.Collections.Generic;
using IgniteTrucksManager.Core.Models;

namespace IgniteTrucksManager.Core.Repo
{
    /// <summary>
    /// Trucks repository interface.
    /// </summary>
    public interface ITrucksRepository
    {
        /// <summary>
        /// Adds a truck to a storage.
        /// </summary>
        /// <param name="truck">Truck model.</param>
        void Save(Truck truck);

        /// <summary>
        /// Gets a truck from a storage.
        /// </summary>
        /// <param name="id">Truck identifier.</param>
        /// <returns>Truck model</returns>
        Truck Get(int id);

        /// <summary>
        /// Gets all trucks.
        /// </summary>
        /// <returns>Truck model</returns>
        IEnumerable<Truck> GetAll();
    }
}
