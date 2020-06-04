using System;
using System.Collections.Generic;

namespace IgniteTrucksManager.Core.Models
{
    /// <summary>
    /// Truck object.
    /// </summary>
    public class Truck
    {
        /// <summary>
        /// Identity.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Registration number.
        /// </summary>
        public string Number { get; }

        /** Internal sensor data collection. */
        private readonly List<SensorData> _data = new List<SensorData>();

        /// <summary>
        /// Related sensor data
        /// </summary>
        public IEnumerable<SensorData> Data => _data;
    

        /// <summary>
        /// Constructor.
        /// </summary>
        public Truck(string number)
        {
            Id = Guid.NewGuid();
            Number = number;
        }

        /// <summary>
        /// Save new sensor items.
        /// </summary>
        /// <param name="data">Sensor data.</param>
        public Truck AddSensorData(IEnumerable<SensorData> data)
        {
            _data.AddRange(data);

            return this;
        }

        public override string ToString()
        {
            return $"Truck [Id={Id}, Number={Number}, Sensor events count={_data.Count}]";
        }
    }
}
