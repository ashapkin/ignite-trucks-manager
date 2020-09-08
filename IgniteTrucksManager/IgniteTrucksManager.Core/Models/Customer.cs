using System;

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
        public string Name { get; set; }
        /// <summary>
        /// Customer's account balance.
        /// </summary>
        public decimal Balance { get; set; }
    }
}
