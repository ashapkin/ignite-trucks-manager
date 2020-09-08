using System;
using Apache.Ignite.Core;
using IgniteTrucksManager.Core.Models;

namespace IgniteTrucksManager.Core.Repo
{
    /// <summary>
    /// Drivers repository.
    /// </summary>
    public class TripsRepository : IgniteRepositoryBase<Guid, Trip>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="ignite">Ignite instance.</param>
        public TripsRepository(IIgnite ignite) : base(ignite, "Trips")
        { }
    }
}
