using System;

namespace IgniteTrucksManager.Core.Models
{
    /// <summary>
    /// A truck's monitoring data.
    /// </summary>
    public sealed class SensorData
    {
        /// <summary>
        /// Record date time, UTC.
        /// </summary>
        public DateTime DateTimeUtc { get; set; }

        /// <summary>
        /// Fuel level.
        /// </summary>
        public double FuelLevel { get; set; }

        /// <summary>
        /// Current speed.
        /// </summary>
        public double Speed { get; set; }
    }
}
