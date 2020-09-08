using System;

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
        public DateTime RequestDateTime { get; set; }
        /// <summary>
        /// Pickup time.
        /// </summary>
        public DateTime? PickUpDateTime { get; set; }
        /// <summary>
        /// Finish time.
        /// </summary>
        public DateTime? FinishDateTime { get; set; }

        /// <summary>
        /// Driver.
        /// </summary>
        public Guid? DriverId { get; set; }
       
        /// <summary>
        /// Customer.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// From location.
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// To location.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Rating
        /// </summary>
        public double Rating { get; set; }
    }
}
