using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IgniteTrucksManager.Core.Models;

namespace IgniteTrucksManager.Core.Repo
{
    /// <summary>
    /// Repository.
    /// </summary>
    /// <typeparam name="TKey">Key.</typeparam>
    /// <typeparam name="TValue">Value.s</typeparam>
    public interface IRepository<in TKey, TValue> where TValue : ModelBase<TKey>
    {
        /// <summary>
        /// Saves the value.
        /// </summary>
        /// <param name="value">Value instance.</param>
        void Save(TValue value);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="key">Value identifier.</param>
        /// <returns>The stored value or <code>Null</code>.</returns>
        TValue Get(TKey key);

        /// <summary>
        /// Gets all values.
        /// </summary>
        /// <returns>The stored values set.</returns>
        IEnumerable<TValue> GetAll();

        /// <summary>
        /// Runs SQL query.
        /// </summary>
        /// <param name="sql">Sql.</param>
        /// <returns>Query result.</returns>
        IList<object> Query(string sql);

        /// <summary>
        /// Runs SQL query.
        /// </summary>
        /// <param name="sql">Sql.</param>
        /// <param name="args">Args.</param>
        /// <returns>Query result.</returns>
        IList<object> Query(string sql, params object[] args);
    }
}
