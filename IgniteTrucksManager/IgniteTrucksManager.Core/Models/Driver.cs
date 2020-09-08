using System;

namespace IgniteTrucksManager.Core.Models
{
    /// <summary>
    /// Driver model.
    /// </summary>
    public class Driver : ModelBase<Guid>
    {
        /// <summary>
        /// Driver's name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Current rating.
        /// </summary>
        public double Rating { get; set; }
        /// <summary>
        /// Driver's account balance.
        /// </summary>
        public decimal Balance { get; set; }
    }
}
