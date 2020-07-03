using System;
using System.Collections.Generic;

namespace IgniteTrucksManager.Core.ComputeTasks.FuelConsumption
{
    /// <summary>
    /// Fuel consumption analyzer.
    /// </summary>
    public class FuelConsumptionAnalyzer
    {
        /** Threshold value. */
        private static readonly double Threshold = 10;

        /// <summary>
        /// Checks whether the level change was smooth enough.
        /// For simplicity we will all values and see whether the next
        /// item differs from current higher then some threshold.
        /// In real life it's better to have check for moving average etc.
        /// </summary>
        /// <param name="levels">Ordered levels.</param>
        /// <returns>Return True if no malfunctions detected, False otherwise. </returns>
        public static bool IsValid(ICollection<double> levels)
        {
            using var enumerator = levels.GetEnumerator();
            using var enumeratorSecond = levels.GetEnumerator();

            if (!enumeratorSecond.MoveNext())
            {
                return true;
            }

            while (enumerator.MoveNext() && enumeratorSecond.MoveNext())
            {
                double diff = Math.Abs(enumerator.Current - enumeratorSecond.Current);
                return diff > Threshold;
            }

            return true;
        }
    }
}
