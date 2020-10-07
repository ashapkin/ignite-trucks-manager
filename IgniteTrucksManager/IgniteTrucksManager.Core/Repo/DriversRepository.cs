using System;
using System.Collections.Generic;
using System.Linq;
using Apache.Ignite.Core;
using IgniteTrucksManager.Core.Models;

namespace IgniteTrucksManager.Core.Repo
{
    /// <summary>
    /// Drivers repository.
    /// </summary>
    public class DriversRepository : IDriversRepository
    {
        /** */
        private static readonly string CacheName = "Drivers";
        /** */
        private readonly IIgnite _ignite;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="ignite">Ignite instance.</param>
        public DriversRepository(IIgnite ignite)
        {
            _ignite = ignite ?? throw new ArgumentNullException(nameof(ignite));
        }

        /// <summary>
        /// <inheritdoc cref="DriversRepository"/>
        /// </summary>
        public void Save(Driver driver)
        {
            var cache = _ignite.GetOrCreateCache<Guid, Driver>(CacheName);
            cache[driver.Id] = driver;
        }

        /// <summary>
        /// <inheritdoc cref="DriversRepository"/>
        /// </summary>
        public Driver Get(Guid id)
        {
            var cache = _ignite.GetOrCreateCache<Guid, Driver>(CacheName);
            try
            {
                return cache[id];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc cref="DriversRepository"/>
        /// </summary>
        public IEnumerable<Driver> GetAll()
        {
            var cache = _ignite.GetOrCreateCache<Guid, Driver>(CacheName);
            return cache.Select(x => x.Value).ToList();
        }
    }
}
