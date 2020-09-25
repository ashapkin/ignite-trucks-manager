using System;
using Apache.Ignite.Core.Cache.Configuration;

namespace IgniteTrucksManager.Core.Models
{
    /// <summary>
    /// Driver model.
    /// </summary>
    public class Driver
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        [QuerySqlField(IsIndexed = true)]
        public Guid Id { get; set; }
        /// <summary>
        /// Driver's name.
        /// </summary>
        [QuerySqlField]
        public string Name { get; set; }
        /// <summary>
        /// Current rating.
        /// </summary>
        [QuerySqlField]
        public double Rating { get; set; }
        /// <summary>
        /// Driver's account balance.
        /// </summary>
        [QuerySqlField]
        public decimal Balance { get; set; }
    }
}