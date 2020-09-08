# Chapter 5

### Introducing the SQL

So far, so good, but let's add more details into our model:

- Driver
- Customer
- Trips

To enable the SQL, we need to convert our existing caches into the corresponded tables.
Unfortunately this can't be done on the fly, we need to recrecate the caches, but since we are dealing
with in-memoty configuraiton and do not worry about possible data loss, we are good. 

The QueryEnity specification describes the table representation for a cache (a SQL table will be created as the result).

Let's modify our repository in a such a way, that it will take care about configuing the QueryEntity automatically:

```
 Cache = new Lazy<ICache<TKey, TValue>>(() =>
            {
                var cacheCfg = new CacheConfiguration(cacheName, new QueryEntity(typeof(TKey), typeof(TValue)));
                return ignite.GetOrCreateCache<TKey, TValue>(cacheCfg);
            });
```

Now the table is defined, but we need to add columns to our definition, there are multiple ways to do it, for 
sample we can extend our CacheConfiguration with QuerySqlField set, but for simplicity, let's add QuerySqlFieldAttribute
attributes to our fields.

Note, that by default, table name will be resolved as our model name, singular, for sample - Driver.

Add an ugly method to our Driver's controller and try running the query:

drivers/query?sql=select * from driver



