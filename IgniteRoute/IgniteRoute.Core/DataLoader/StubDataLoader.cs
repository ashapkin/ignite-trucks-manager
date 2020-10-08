using System;
using System.Collections.Generic;
using IgniteRoute.Core.Models;
using IgniteRoute.Core.Repo;

namespace IgniteRoute.Core.DataLoader
{
    /// <summary>
    /// An interface that pulls data from an external source and saves it in Ignite.
    /// </summary>
    public class StubDataLoader : IDataLoader
    {
        /** */
        private readonly IDriversRepository _repository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository.</param>
        public StubDataLoader(IDriversRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// <inheritdoc cref="IDataLoader.LoadInitialData" />
        /// </summary>
        public void LoadInitialData()
        {
            foreach (Driver driver in GetInitialDrivers())
            {
                _repository.Save(driver);
            }
        }

        /// <summary>
        /// Generates divers.
        /// </summary>
        private static IEnumerable<Driver> GetInitialDrivers()
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
