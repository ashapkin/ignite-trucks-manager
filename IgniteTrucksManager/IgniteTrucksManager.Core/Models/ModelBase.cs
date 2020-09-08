using System;
using Apache.Ignite.Core.Cache.Configuration;

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
        [QuerySqlField(IsIndexed = true)]
        public TKey Id { get; set; }
    }
}
