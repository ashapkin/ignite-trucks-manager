using System;
using System.Collections.Generic;
using System.Linq;

namespace IgniteTrucksManager.Core.Models
{
    /// <summary>
    /// Truck object.
    /// </summary>
    public class Truck
    {
        /// <summary>
        /// Registration id.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Related sensor data
        /// </summary>
        public List<SensorData> Data { get; } = new List<SensorData>();


        /// <summary>
        /// Constructor.
        /// </summary>
        public Truck(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Save new sensor items.
        /// </summary>
        /// <param name="data">Sensor data.</param>
        public Truck AddSensorData(IEnumerable<SensorData> data)
        {
            DateTime? startDate = Data.LastOrDefault()?.DateTimeUtc;

            var ordered = data.OrderBy(x => x.DateTimeUtc).ToList();

            if (startDate == null || ordered.FirstOrDefault()?.DateTimeUtc > startDate)
            {
                Data.AddRange(ordered);
            }

            return this;
        }

        /// <summary>
        /// <inheritdoc cref="object"/>
        /// </summary>
        public override string ToString()
        {
            return $"Truck [Id={Id},  Sensor events count={Data.Count}]";
        }
    }
}
