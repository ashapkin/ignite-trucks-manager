using System;
using System.Collections.Generic;
using System.Linq;
using Apache.Ignite.Core;
using Apache.Ignite.Core.Cache;
using Apache.Ignite.Core.Cache.Configuration;
using Apache.Ignite.Core.Cache.Query;
using IgniteTrucksManager.Core.Models;

namespace IgniteTrucksManager.Core.Repo
{
    /// <summary>
    /// Ignite-based abstract repository.
    /// </summary>
    /// <typeparam name="TKey">Key type.</typeparam>
    /// /// <typeparam name="TValue">Value type.</typeparam>
    public abstract class IgniteRepositoryBase<TKey, TValue> : IRepository<TKey, TValue> where TValue : ModelBase<TKey>
    {
        /// <summary>
        /// Ignite cache instance.
        /// </summary>
        protected Lazy<ICache<TKey, TValue>> Cache { get; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="ignite">Ignite instance.</param>
        /// <param name="cacheName">Cache name.</param>
        protected IgniteRepositoryBase(IIgnite ignite, string cacheName)
        {
            if (string.IsNullOrWhiteSpace(cacheName))
            {
                throw new ArgumentException("cache name is empty", nameof(cacheName));
            }

            if (ignite == null)
            {
                throw new ArgumentNullException(nameof(ignite));
            }

            Cache = new Lazy<ICache<TKey, TValue>>(() =>
            {
                var cacheCfg = new CacheConfiguration(cacheName, new QueryEntity(typeof(TKey), typeof(TValue)));
                return ignite.GetOrCreateCache<TKey, TValue>(cacheCfg);
            });
        }

        /// <summary>
        /// <inheritdoc cref="IRepository{TKey,TValue}.Save"/>
        /// </summary>
        public virtual void Save(TValue value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Cache.Value.Put(value.Id, value);
        }

        /// <summary>
        /// <inheritdoc cref="IRepository{TKey,TValue}.Get"/>
        /// </summary>
        public virtual TValue Get(TKey key)
        {
            try
            {
                return Cache.Value.Get(key);
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc cref="IRepository{TKey,TValue}.GetAll"/>
        /// </summary>
        public virtual IEnumerable<TValue> GetAll()
        {
            return Cache.Value.Select(x => x.Value).ToList();
        }

        /// <summary>
        /// <inheritdoc cref="IRepository{TKey,TValue}.Query"/>
        /// </summary>
        public object Query(string sql)
        {
            return Cache.Value.Query(new SqlFieldsQuery(sql));
        }
    }
}
