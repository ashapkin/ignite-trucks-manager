using System;
using System.Collections.Generic;
using System.Linq;
using Apache.Ignite.Core;
using IgniteTrucksManager.Core.Models;

namespace IgniteTrucksManager.Core.Repo
{
    /// <summary>
    /// Trucks repository.
    /// </summary>
    public class TrucksRepository : ITrucksRepository
    {
        /** */
        private static readonly string CacheName = "Trucks";
        /** */
        private readonly IIgnite _ignite;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="ignite">Ignite instance.</param>
        public TrucksRepository(IIgnite ignite)
        {
            _ignite = ignite ?? throw new ArgumentNullException(nameof(ignite));
        }

        /// <summary>
        /// <inheritdoc cref="TrucksRepository"/>
        /// </summary>
        public void Save(Truck truck)
        {
            var cache = _ignite.GetOrCreateCache<Guid, Truck>(CacheName);
            cache[truck.Id] = truck;
        }

        /// <summary>
        /// <inheritdoc cref="TrucksRepository"/>
        /// </summary>
        public Truck Get(Guid id)
        {
            var cache = _ignite.GetOrCreateCache<Guid, Truck>(CacheName);
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
        /// <inheritdoc cref="TrucksRepository"/>
        /// </summary>
        public IEnumerable<Truck> GetAll()
        {
            var cache = _ignite.GetOrCreateCache<Guid, Truck>(CacheName);
            return cache.Select(x => x.Value).ToList();
        }
    }
}
