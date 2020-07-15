# Chapter 2

### Setting up ASP.NET Core

Let's create a new ASP.NET project - `IgniteTrucksManager.Api`.
This project should expose the application API and manage HTTP requests.

First of all we need to configure the DI so we could have access to our data layers:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IIgnite, IIgnite>(serviceProvider => Ignition.Start());
    services.AddSingleton<ITrucksRepository, TrucksRepository>();
    services.AddSingleton<ExternalDataProvider, ExternalDataProvider>();
    services.AddControllers();
}
```

Now we need a couple of controllers for initial data processing: `DataLoaderController.cs` and `TrucksController.cs`
The `DataLoaderController` will simulate quering an external API and saving the data in Ignite.
```csharp
/// <summary>
/// Loads external data. For simplicity, marked as GET.
/// </summary>
[HttpGet]
public string Get()
{
    _dataProvider.PullNewData();
    return "Completed!";
}
```

The `TrucksController` then will read persisted values:
```csharp
/// <summary>
/// Gets trucks. For simlicity, marked as GET.
/// </summary>
[HttpGet]
public IEnumerable<Truck> Get()
{
    return _repository.GetAll();
}
```

Within `IgniteTrucksManager.Core` project we can simulate the external provider with the `ExternalDataProvider.cs` that 
will generate values out of `TrucksData.GetData()` method:
```csharp
/// <summary>
/// Pulls the new data and save it to internal storage.
/// </summary>
public void PullNewData()
{
    IDictionary<Truck, SensorData[]> newData = TrucksData.GetData();

    foreach (KeyValuePair<Truck, SensorData[]> item in newData)
    {
        Truck truck = _repository.Get(item.Key.Id) ?? item.Key;
        truck.AddSensorData(item.Value);

        _repository.Save(truck);
    }
}
```

Now we can generate the values and store them within Ignite. We just iterating over the input values and save them in Ignite individually.
This approach works fine for an arbitrarily small number of values, but it's always better to batch operations and avoid unnecessary network and serialization roundtrips.

A possible solution would be to either user `cach#putAll API` or to utilize the [DataStreamer API](https://ignite.apache.org/features/streaming.html).