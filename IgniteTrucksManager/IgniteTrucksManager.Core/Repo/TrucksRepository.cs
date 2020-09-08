﻿using Apache.Ignite.Core;
using IgniteTrucksManager.Core.Models;

namespace IgniteTrucksManager.Core.Repo
{
    /// <summary>
    /// Trucks repository.
    /// </summary>
    public class TrucksRepository : IgniteRepositoryBase<int, Truck>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="ignite">Ignite instance.</param>
        public TrucksRepository(IIgnite ignite) : base(ignite, "Trucks")
        { }
    }
}
