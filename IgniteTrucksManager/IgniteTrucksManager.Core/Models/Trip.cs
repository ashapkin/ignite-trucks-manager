using System;
using Apache.Ignite.Core.Cache.Configuration;

namespace IgniteTrucksManager.Core.Models
{
    /// <summary>
    /// Trip model.
    /// </summary>
    public class Trip : ModelBase<Guid>
    {
        /// <summary>
        /// Request time.
        /// </summary>
        [QuerySqlField]
        public DateTime RequestDateTime { get; set; }
        /// <summary>
        /// Finish time.
        /// </summary>
        [QuerySqlField]
        public DateTime? FinishDateTime { get; set; }

        /// <summary>
        /// Driver.
        /// </summary>
        [QuerySqlField]
        public Guid? DriverId { get; set; }

        /// <summary>
        /// Customer.
        /// </summary>
        [QuerySqlField]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        [QuerySqlField]
        public decimal Price { get; set; }

        /// <summary>
        /// From location.
        /// </summary>
        [QuerySqlField]
        public string From { get; set; }
        /// <summary>
        /// To location.
        /// </summary>
        [QuerySqlField]
        public string To { get; set; }

        /// <summary>
        /// Rating
        /// </summary>
        [QuerySqlField]
        public double Rating { get; set; }
    }
}
