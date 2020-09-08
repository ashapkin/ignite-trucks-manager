using System;
using Apache.Ignite.Core;
using IgniteTrucksManager.Core.Models;

namespace IgniteTrucksManager.Core.Repo
{
    /// <summary>
    /// Drivers repository.
    /// </summary>
    public class DriversRepository : IgniteRepositoryBase<Guid, Driver>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="ignite">Ignite instance.</param>
        public DriversRepository(IIgnite ignite) : base(ignite, "Drivers")
        { }
    }
}
