using System;
using Apache.Ignite.Core;
using IgniteRoute.Core.Models;
using IgniteRoute.Core.Repo;
using IgniteRoute.Core.Tests.Utils;
using NUnit.Framework;
using FluentAssertions;
using IgniteRoute.Core.DataLoader;

namespace IgniteRoute.Core.Tests.Repo
{
    /// <summary>
    /// Drivers repository tests.
    /// </summary>
    public class RepositoriesTests
    {
        private IIgnite _ignite;
        private DriversRepository _driversRepository;
        private StubDataLoader _stubDataLoader;

        /// <summary>
        /// One time set up.
        /// </summary>
        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            _ignite = Ignition.Start(TestUtils.GetTestConfiguration());
            _driversRepository = new DriversRepository(_ignite);
            _stubDataLoader = new StubDataLoader(_driversRepository);
        }

        /// <summary>
        /// One time tear down.
        /// </summary>
        [OneTimeTearDown]
        public void TestFixtureTearDown()
        {
            _ignite.Dispose();
        }

        /// <summary>
        /// Tear down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            foreach (var cacheName in _ignite.GetCacheNames())
            {
                _ignite.DestroyCache(cacheName);   
            }
        }

        /// <summary>
        /// Tests that value stored in Ignite could be read.
        /// </summary>
        [Test]
        public void TestDriversSaveGetMethod()
        {
            var driver = new Driver
            {
                Name = "Joshua",
                Balance = 12.44m,
                Id = Guid.NewGuid(),
                Rating = 4.55
            };

            _driversRepository.Save(driver);

            var stored = _driversRepository.Get(driver.Id);

            driver.Should().BeEquivalentTo(stored);
        }

        /// <summary>
        /// Tests that data loader fill Ignite with stub data.
        /// </summary>
        [Test]
        public void TestSubDataLoader()
        {
            _driversRepository.GetAll().Should().BeEmpty();

            _stubDataLoader.LoadInitialData();

            _driversRepository.GetAll().Should().NotBeEmpty();
        }
    }
}
