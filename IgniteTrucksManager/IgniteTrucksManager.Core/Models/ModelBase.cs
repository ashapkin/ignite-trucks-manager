using System;

namespace IgniteTrucksManager.Core.Models
{

    /// <summary>
    /// Base model representation.
    /// </summary>
    /// <typeparam name="TKey">Key type.</typeparam>
    public abstract class ModelBase<TKey>
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public TKey Id { get; set; }
    }
}
