using System.Threading.Tasks;
using Apache.Ignite.Core;
using IgniteTrucksManager.Core.ComputeTasks.FuelConsumption;

namespace IgniteTrucksManager.Core.ComputeTasks
{
    /// <summary>
    /// Ignite compute gateway.
    /// </summary>
    public class IgniteCompute : IIgniteCompute
    {
        /** */
        private readonly IIgnite _ignite;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ignite">Ignite.</param>
        public IgniteCompute(IIgnite ignite)
        {
            _ignite = ignite;
        }

        /// <summary>
        /// Run fuel consumption analyzer task.
        /// </summary>
        /// <param name="truckId">Truck Id.</param>
        /// <returns>True if fuel consumption is ok, False otherwise.</returns>
        public Task<bool> AnalyzeFuelConsumption(int truckId)
        {
            //how to pass a DI reference here??
            return _ignite.GetCompute().CallAsync(new FuelConsumptionIgniteTask(truckId));
        }
    }
}
