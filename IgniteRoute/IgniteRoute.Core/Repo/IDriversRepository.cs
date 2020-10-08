using System;
using System.Collections.Generic;
using IgniteRoute.Core.Models;

namespace IgniteRoute.Core.Repo
{
    /// <summary>
    /// Drivers repository interface.
    /// </summary>
    public interface IDriversRepository
    {
        /// <summary>
        /// Adds a driver to a storage.
        /// </summary>
        /// <param name="driver">Driver model.</param>
        void Save(Driver driver);

        /// <summary>
        /// Gets a driver from a storage.
        /// </summary>
        /// <param name="id">Driver identifier.</param>
        /// <returns>Driver model</returns>
        Driver Get(Guid id);

        /// <summary>
        /// Gets all drivers.
        /// </summary>
        /// <returns>Driver model</returns>
        IEnumerable<Driver> GetAll();
    }
}
