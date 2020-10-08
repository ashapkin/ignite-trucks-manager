# Chapter 2 - Configuring ASP.NET project

### Setting up ASP.NET Core.

It's time to turn our application from a simple console application to a RESTful API service. Let's add a new project `IgniteRoute.Api` that will expose the application API and handle Http requests. Let's add a `DriversController` that will responsible for CRUD operation with the `Driver` model.

```csharp
using System.Collections.Generic;
using IgniteRoute.Core.Models;
using IgniteRoute.Core.Repo;
using Microsoft.AspNetCore.Mvc;

namespace IgniteRoute.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly IDriversRepository _repository;

        public DriversController(IDriversRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("add")]
        public Driver Add(string name)
        {
            var driver = new Driver {Name = name, Id = Guid.NewGuid()};
            _repository.Save(driver);
            return driver;
        }

        [HttpGet]
        public IEnumerable<Driver> Get()
        {
            return _repository.GetAll();
        }
    }
}
```

Next, we need to add a DI mapping for `IDriversRepository` and `IIgnite` interface inside the `Startup.cs`:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IIgnite, IIgnite>(serviceProvider => Ignition.Start());
    services.AddSingleton<IDriversRepository, DriversRepository>();
    services.AddControllers();
}
```

Now we can test our controller using the following URLs (depending on your ports configuration):
```
https://localhost:44310/drivers
https://localhost:44310/drivers/add?name=John
{"id":"e22b19f5-acb8-441f-a715-efb45bb71c46","name":"John","rating":0,"balance":0}
```

### Adding a DataProvier.

We don't want to fill a stub data every time, thus let's add a new abstraction `StubDataLoader` that will do this routine for us. For now, we are going to deal with the hardcoded data and store it in Apache Ignite on `LoadInitialData` method call. 

```csharp
public class StubDataLoader : IDataLoader
{
    /** */
    private readonly IDriversRepository _repository;

    public StubDataProvider(IDriversRepository repository)
    {
        _repository = repository;
    }

    public void LoadInitialData()
    {
        foreach (Driver driver in GetInitialDrivers())
        {
            _repository.Save(driver);
        }
    }

    private static IEnumerable<Driver> GetInitialDrivers()
    {
        return new List<Driver>
        {
            new Driver
            {
                Id = Guid.Parse("5da296f4-b944-4eb4-a4fd-08fc4bc7661d"),
                Balance = 300,
                Name = "John",
                Rating = 4
            },
            ...
        };
    }
}
```

The `LoadInitialData` just delegates a call to the `IDriversRepository.Save` method for every generated driver. Sequential cache # put operations inside a foreach loop are definitely not the best option of data loading. Alternatively, we can speed initial data processing with the ʻICache.PutAll` and `DataStreamer` feature of Apache ignite. We will check them in the next chapters.

The last thing to do here is - we need to add a new `DataLoaderController` that will use our new `StubDataLoader` manager:

```csharp
[ApiController]
[Route("[controller]")]
public class DataLoaderController
{
    /** */
    private readonly IDataLoader _dataProvider;

    public DataLoaderController(IDataLoader dataLoader)
    {
        _dataProvider = dataLoader;
    }

    [HttpGet]
    public string Get()
    {
        _dataProvider.LoadInitialData();
        return "Completed!";
    }
}
```

And register additional dependencies:

```csharp
...
services.AddSingleton<IDataLoader, StubDataLoader>();
...
```

### Summary

In this chapter, we reworked our initial console app into a REST API service. We added the `DriversController` and `DataLoaderController`, added a stub-generated data provider, and are able to work with our application from a browser. In real applications, the logic is far too complex and it's important to keep an eye on what's happening with Apache Ignite, therefore in the next chapter we will see how to configure logging for Apache Ignite.