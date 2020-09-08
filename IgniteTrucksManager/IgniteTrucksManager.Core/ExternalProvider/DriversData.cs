using System;
using System.Collections.Generic;
using IgniteTrucksManager.Core.Models;

namespace IgniteTrucksManager.Core.ExternalProvider
{
    /// <summary>
    /// Static drivers data.
    /// </summary>
    internal static class DriversData
    {
        public static IList<Driver> GetDrivers()
        {
            return new List<Driver>
            {
                new Driver
                {
                    Id = Guid.Parse("5da296f4-b944-4eb4-a4fd-08fc4bc7661d"),
                    Balance = 300,
                    Name = "John",
                    Rating = 4
                },
                new Driver
                {
                    Id = Guid.Parse("eb85b1ba-ef36-4645-b70c-2f774d5a1146"),
                    Balance = 8000,
                    Name = "Peter",
                    Rating = 3.5
                },
                new Driver
                {
                    Id = Guid.Parse("570e302b-ff5c-437f-b2d6-65739cc573c9"),
                    Balance = 1000,
                    Name = "Ivan",
                    Rating = 4
                },
            };
        }
    }
}
