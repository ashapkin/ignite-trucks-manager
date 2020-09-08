using System;
using System.Collections.Generic;
using IgniteTrucksManager.Core.Models;

namespace IgniteTrucksManager.Core.ExternalProvider
{
    /// <summary>
    /// External data source simulation.
    /// </summary>
    internal static class TrucksData
    {
        /// <summary>
        /// Gets the external data parsed into the models.
        /// </summary>
        /// <returns>Trucks with theirs sensor data.</returns>
        public static IDictionary<Truck, SensorData[]> GetData()
        {
            var data = new Dictionary<Truck, SensorData[]>
            {
                {
                    new Truck(1), new[]
                    {
                        new SensorData {DateTimeUtc = DateTime.Parse("2020-01-01 12:00:00"), FuelLevel = 100, Speed = 80},
                        new SensorData {DateTimeUtc = DateTime.Parse("2020-01-01 12:01:00"), FuelLevel = 100, Speed = 80},
                        new SensorData {DateTimeUtc = DateTime.Parse("2020-01-01 12:02:00"), FuelLevel = 100, Speed = 75},
                        new SensorData {DateTimeUtc = DateTime.Parse("2020-01-01 12:03:00"), FuelLevel = 99, Speed = 80},
                        new SensorData {DateTimeUtc = DateTime.Parse("2020-01-01 12:04:00"), FuelLevel = 99, Speed = 82},
                        new SensorData {DateTimeUtc = DateTime.Parse("2020-01-01 12:05:00"), FuelLevel = 99, Speed = 81},
                        new SensorData {DateTimeUtc = DateTime.Parse("2020-01-01 12:06:00"), FuelLevel = 98, Speed = 50},
                        new SensorData {DateTimeUtc = DateTime.Parse("2020-01-01 12:07:00"), FuelLevel = 97, Speed = 5},
                        new SensorData {DateTimeUtc = DateTime.Parse("2020-01-01 12:08:00"), FuelLevel = 97, Speed = 0},
                        new SensorData {DateTimeUtc = DateTime.Parse("2020-01-01 12:09:00"), FuelLevel = 97, Speed = 0},
                        new SensorData {DateTimeUtc = DateTime.Parse("2020-01-01 12:10:00"), FuelLevel = 96, Speed = 0},
                        new SensorData {DateTimeUtc = DateTime.Parse("2020-01-01 12:30:00"), FuelLevel = 60, Speed = 0},
                        new SensorData {DateTimeUtc = DateTime.Parse("2020-01-01 12:13:00"), FuelLevel = 60, Speed = 10},
                        new SensorData {DateTimeUtc = DateTime.Parse("2020-01-01 12:14:00"), FuelLevel = 60, Speed = 80},
                        new SensorData {DateTimeUtc = DateTime.Parse("2020-01-01 12:15:00"), FuelLevel = 60, Speed = 90},
                    }
                }
            };


            return data;
        }
    }
}
