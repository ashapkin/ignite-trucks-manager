using System;
using Apache.Ignite.Core.Cache.Configuration;

namespace IgniteTrucksManager.Core.Models
{
    /// <summary>
    /// Customer.
    /// </summary>
    public class Customer : ModelBase<Guid>
    {
        /// <summary>
        /// Customer's name.
        /// </summary>
        [QuerySqlField]
        public string Name { get; set; }
        /// <summary>
        /// Customer's account balance.
        /// </summary>
        [QuerySqlField]
        public decimal Balance { get; set; }
    }
}
