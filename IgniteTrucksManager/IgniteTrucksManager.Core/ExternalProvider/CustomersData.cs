using System;
using System.Collections.Generic;
using IgniteTrucksManager.Core.Models;

namespace IgniteTrucksManager.Core.ExternalProvider
{
    /// <summary>
    /// Static customers data.
    /// </summary>
    internal static class CustomersData
    {
        /// <summary>
        /// Generates customers.
        /// </summary>
        /// <returns>Customers.</returns>
        public static IList<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    Id = Guid.Parse("a5085a58-e732-4f17-988d-65b05b2a8b6a"),
                    Name = "Angelia",
                    Balance = 200
                },
                new Customer
                {
                    Id = Guid.Parse("6242cbfa-1b45-4dc2-bce6-5378d384f3ca"),
                    Name = "Alex",
                    Balance = 2000
                },
                new Customer
                {
                    Id = Guid.Parse("0f2bbada-b7d6-4521-b4fb-8f949f1d04b1"),
                    Name = "Mary",
                    Balance = 0
                },
                new Customer
                {
                    Id = Guid.Parse("3e15d4dc-8433-4b83-9a72-915cd7232a32"),
                    Name = "Sarah",
                    Balance = 500
                },
                new Customer
                {
                    Id = Guid.Parse("ef541c82-df60-421e-bebb-2d9e5ed5314f"),
                    Name = "William",
                    Balance = 10
                }
            };
        }
    }
}
