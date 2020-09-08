# Chapter 5

### Introducing the SQL

So far, so good, but let's add more details into our model:

- Driver
- Customer
- Trips

```csharp
while (enumerator.MoveNext() && enumeratorSecond.MoveNext())
{
    double diff = Math.Abs(enumerator.Current - enumeratorSecond.Current);
    return diff > Threshold;
}
```

We might iterate over the whole collection using the `cache#get` API, but this will require the whole data set to be transferred from a server to a client node. We might avoid it using the `ComputeGrid` API and send the compute task itself (source code) to a server rather than a data collection. More details: [distributed-computing](https://www.gridgain.com/docs/latest/developers-guide/distributed-computing/distributed-computing).

To do this, we declared the `IgniteCompute.cs` facade that will utilize the appropriate Ignite API. The `#AnalyzeFuelConsumption` method accepts a truckId, instantiate a new `FuelConsumptionIgniteTask.c`s object and calls `ignite.GetCompute().CallAsync(task)`.

```csharp
/// <summary>
/// Run fuel consumption analyzer task.
/// </summary>
/// <param name="truckId">Truck Id.</param>
/// <returns>True if fuel consumption is ok, False otherwise.</returns>
public Task<bool> AnalyzeFuelConsumption(int truckId)
{
    return _ignite.GetCompute().CallAsync(new FuelConsumptionIgniteTask(truckId));
}
```

Under the hood, Ignite serializes the compute task into a binary format (with all other fields, so be careful here) and sends it to a server node. The node then deserializes the task and executes it, sending back a result, in our case, it's `bool` object. 

Note, the current approach has a drawback: it's not obvious how can we use a DI framework within a callable. For sample, Ignite's internals allows us to mark an Ignite instance with `[InstanceResourceAttribute]`, thus the instance will be auto injected on task deserialization. We can benefit from using a custom plugin and register a DI resolver instance within it.  We will check this approach later on. 