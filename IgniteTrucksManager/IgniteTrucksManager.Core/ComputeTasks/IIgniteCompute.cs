using System.Threading.Tasks;

namespace IgniteTrucksManager.Core.ComputeTasks
{
    /// <summary>
    /// Ignite computations.
    /// </summary>
    public interface IIgniteCompute
    {
        /// <summary>
        /// Run fuel consumption analyzer task.
        /// </summary>
        /// <param name="truckId">Truck Id.</param>
        /// <returns>True if fuel consumption is ok, False otherwise.</returns>
        Task<bool> AnalyzeFuelConsumption(int truckId);
    }
}
