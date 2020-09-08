using System;
using Apache.Ignite.Core;
using IgniteTrucksManager.Core.Models;

namespace IgniteTrucksManager.Core.Repo
{
    /// <summary>
    /// Drivers repository.
    /// </summary>
    public class CustomersRepository : IgniteRepositoryBase<Guid, Customer>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="ignite">Ignite instance.</param>
        public CustomersRepository(IIgnite ignite) : base(ignite, "Customers")
        { }
    }
}
