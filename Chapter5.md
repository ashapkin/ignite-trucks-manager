# Chapter 5

### Working with Apache Ignite SQL using .NET

Now it's time to get back to our application and extend it with some additional logic as you remember...
What we will do in chapter - creating a simple trip manager, that'll allow us to create/assign driver and finish orders.

#### Turning our caches into SQL tables

Cache API is not the only way of working with a cache, in Apache Ignite you can easily define you SQL tables and work with them just like with a relational DBs.

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

#### Performing queries

Add an ugly method to our Driver's controller and try running the query:

drivers/query?sql=select * from driver

The question: 
WHY CANT WE JUST USE SOME KIND OF ATTRIBUTES FOR QUERY ENTITY SPECIFICATION?

Ok, let's try to specify our quiery right in-place:
```
        [HttpGet]
        [Route("topdrivers")]
        public object TopDriversSql()
        {
            _logger.LogDebug("Getting top drivers");

            var sql = "select Name, Rating from Driver order by Rating desc";

            return _repository.Query(sql);
        }
```

#### Summary

Now we can do some analytic for our data. The .NET framework support a well-known LINQ feature, the Apache Ignite have a LINQ extention as well,
thus let's add it to our project in the next chapter.




