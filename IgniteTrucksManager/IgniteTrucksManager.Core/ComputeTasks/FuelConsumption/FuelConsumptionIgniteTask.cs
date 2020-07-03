using System.Linq;
using Apache.Ignite.Core;
using Apache.Ignite.Core.Compute;
using Apache.Ignite.Core.Resource;
using IgniteTrucksManager.Core.Models;
using IgniteTrucksManager.Core.Repo;

namespace IgniteTrucksManager.Core.ComputeTasks.FuelConsumption
{
    /// <summary>
    /// Fuel consumption Ignite task.
    /// </summary>
    internal class FuelConsumptionIgniteTask : IComputeFunc<bool>
    {
        /** */
        private readonly int _truckId;

        [InstanceResource]
        //todo: implement DI resolver
#pragma warning disable 649
        private IIgnite _ignite;
#pragma warning restore 649

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="truckId"></param>
        public FuelConsumptionIgniteTask(int truckId)
        {
            _truckId = truckId;
        }

        /// <summary>
        /// <inheritdoc cref="IComputeFunc{TRes}"/>
        /// </summary>
        public bool Invoke()
        {
            var repo = new TrucksRepository(_ignite);

            Truck truck = repo.Get(_truckId);
            var data = truck.Data.Select(x => x.FuelLevel).ToList();
            
            return FuelConsumptionAnalyzer.IsValid(data);
        }
    }
}
