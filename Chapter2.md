# Chapter 2

### Setting up ASP.NET Core

#### Addinig controllers

Let's create a new ASP.NET project - `IgniteTrucksManager.Api`.
This project should expose the application API and manage HTTP requests.

First of all we need a couple of controllers for initial data processing: `DataLoaderController.cs` and `DriversController.cs`
The `DataLoaderController` will simulate quering an external API and saving the data in Ignite.
```csharp
/// <summary>
/// Loads external data. For simplicity, marked as GET.
/// </summary>
[HttpGet]
public string Get()
{
    _logger.LogDebug("Loading data");

    _dataProvider.PullNewData();
    return "Completed!";
}
```

The `DriversController` then will read persisted values:
```csharp
/// <summary>
/// Gets drivers. For simlicity, marked as GET.
/// </summary>
[HttpGet]
public IEnumerable<Driver> Get()
{
    return _repository.GetAll();
}
```


#### Configure Dependency Injetion

Now we need to adjust our DI setting for `Ignite` and our `DriversRepository` repository so we could have access to our data layers.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IIgnite, IIgnite>(serviceProvider => Ignition.Start());
    services.AddSingleton<IDriversRepository, DriversRepository>();
    services.AddSingleton<ExternalDataProvider, ExternalDataProvider>();
    services.AddControllers();
}
```
Within `IgniteTrucksManager.Core` project  we defined `ExternalDataProvider` for hardcoded data sample, so we won't generate them manually using`DriversData.GetDrivers()` method:

```csharp
/// <summary>
/// Pulls the new data and save it to internal storage.
/// </summary>
public void PullNewData()
{
    foreach (Driver driver in DriversData.GetDrivers())
    {
        _repository.Save(driver);
    }
}
```

#### Summary

Now we can generate the values and store them within Ignite. We just iterating over the input values and save them in Ignite individually.
This approach works fine for an arbitrarily small number of values, but it's always better to batch operations and avoid unnecessary network and serialization roundtrips.

A possible solution would be to either user `cach#putAll API` or to utilize the [DataStreamer API](https://ignite.apache.org/features/streaming.html).
In the next chapter we will check how to do it.
