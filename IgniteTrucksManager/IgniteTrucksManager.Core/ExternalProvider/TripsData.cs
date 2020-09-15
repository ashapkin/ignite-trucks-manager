using System;
using System.Collections.Generic;
using IgniteTrucksManager.Core.Models;

namespace IgniteTrucksManager.Core.ExternalProvider
{
    /// <summary>
    /// Static
    /// </summary>
    internal static class TripsData
    {
        public static IList<Trip> GetTrips()
        {
            return new List<Trip>
            {
                new Trip
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Guid.Parse("a5085a58-e732-4f17-988d-65b05b2a8b6a"),
                    DriverId = Guid.Parse("5da296f4-b944-4eb4-a4fd-08fc4bc7661d"),
                    RequestDateTime = DateTime.Parse("2020/07/08 15:10:20").ToUniversalTime(),
                    From = "Point1",
                    To = "Point2",
                    FinishDateTime = DateTime.Parse("2020/07/08 15:30:20").ToUniversalTime(),
                    Price = 55,
                    Rating = 4
                },
                new Trip
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Guid.Parse("a5085a58-e732-4f17-988d-65b05b2a8b6a"),
                    DriverId = Guid.Parse("eb85b1ba-ef36-4645-b70c-2f774d5a1146"),
                    RequestDateTime = DateTime.Parse("2020/07/09 10:10:20").ToUniversalTime(),
                    From = "Point1",
                    To = "Point2",
                    FinishDateTime = DateTime.Parse("2020/07/09 14:30:20").ToUniversalTime(),
                    Price = 260,
                    Rating = 2
                },
                new Trip
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Guid.Parse("0f2bbada-b7d6-4521-b4fb-8f949f1d04b1"),
                    DriverId = Guid.Parse("570e302b-ff5c-437f-b2d6-65739cc573c9"),
                    RequestDateTime = DateTime.Parse("2020/07/08 11:10:20").ToUniversalTime(),
                    From = "Point1",
                    To = "Point2",
                    FinishDateTime = DateTime.Parse("2020/07/08 11:50:20").ToUniversalTime(),
                    Price = 105,
                    Rating = 4
                },
                new Trip
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Guid.Parse("ef541c82-df60-421e-bebb-2d9e5ed5314f"),
                    DriverId = Guid.Parse("eb85b1ba-ef36-4645-b70c-2f774d5a1146"),
                    RequestDateTime = DateTime.Parse("2020/07/08 16:22:20").ToUniversalTime(),
                    From = "Point1",
                    To = "Point2",
                    FinishDateTime = DateTime.Parse("2020/07/08 17:50:20").ToUniversalTime(),
                    Price = 660,
                    Rating = 5
                }
            };
        }
    }
}
