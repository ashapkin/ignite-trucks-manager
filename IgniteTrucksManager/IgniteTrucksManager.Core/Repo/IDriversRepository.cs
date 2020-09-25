using System;
using System.Collections.Generic;
using IgniteTrucksManager.Core.Models;

namespace IgniteTrucksManager.Core.Repo
{
    /// <summary>
    /// Drivers repository interface.
    /// </summary>
    public interface IDriversRepository
    {
        /// <summary>
        /// Adds a driver to a storage.
        /// </summary>
        /// <param name="truck">Driver model.</param>
        void Save(Driver truck);

        /// <summary>
        /// Gets a driver from a storage.
        /// </summary>
        /// <param name="id">Truck identifier.</param>
        /// <returns>Driver model</returns>
        Driver Get(Guid id);

        /// <summary>
        /// Gets all drivers.
        /// </summary>
        /// <returns>Driver model</returns>
        IEnumerable<Driver> GetAll();
    }
}
