using System;
using System.Collections.Generic;
using System.Linq;
using Apache.Ignite.Core;
using IgniteRoute.Core.Models;

namespace IgniteRoute.Core.Repo
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
        /// <inheritdoc cref="IDriversRepository.Save"/>
        /// </summary>
        public void Save(Driver driver)
        {
            var cache = _ignite.GetOrCreateCache<Guid, Driver>(CacheName);
            cache[driver.Id] = driver;
        }

        /// <summary>
        /// <inheritdoc cref="IDriversRepository.Get"/>
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
        /// <inheritdoc cref="IDriversRepository.GetAll"/>
        /// </summary>
        public IEnumerable<Driver> GetAll()
        {
            var cache = _ignite.GetOrCreateCache<Guid, Driver>(CacheName);
            return cache.Select(x => x.Value).ToList();
        }
    }
}
