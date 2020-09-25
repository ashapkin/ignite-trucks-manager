# Chapter 1

### Apache Ignite installation.

Let's start creating a new .NET Core 3.1 console project - `IgniteTrucksManager.Core` and installing the following Nuget package:
```xml
<ItemGroup>
  <PackageReference Include="Apache.Ignite" Version="2.8.0" />
</ItemGroup>
```

Note, that after the installation, there are should be two folders within your project: `config` with the default java logging properties
and `libs` with java packages. Apache Ignite strongly relies on Java ecosystem thus is mandatory to install some kind of JRE, please, 
refer to the [official docs](https://apacheignite-net.readme.io/docs/troubleshooting) for more details. Most of the common pitfalls are describe at the [troubleshooting docs](https://apacheignite-net.readme.io/docs/troubleshooting) as well.

Let's verify the installation by creating a `Program.cs` file with that spawns an Ignite process:
```csharp
static void Main(string[] args)
{
    using (var ignite = Ignition.Start())
    {
       Console.WriteLine("Hello World!")               
    }
}
```

You should be able to see similar console output:
```
[23:13:50,463][INFO][main][PluginProcessor]   ^-- None
Hello World!
```

### Working with a cache

Now it's time to deine our business models. Lets keep it simple and define a simple POCO model `Driver.cs`. No additional configuration is required at the moment.

```csharp
/// <summary>
/// Driver model.
/// </summary>
public class Driver : ModelBase<Guid>
{
    /// <summary>
    /// Driver's name.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Current rating.
    /// </summary>
    public double Rating { get; set; }
    /// <summary>
    /// Driver's account balance.
    /// </summary>
    public decimal Balance { get; set; }
}
```

Let's add the `IDriversRepository` abstraction that will deal with Apache Ignite directly. We need to define a simple data access methods: `Get` and `Add`.
Here is the repository implementation:
```csharp
public class DriversRepository : IDriverssRepository
{
    /** */
    private static readonly string CacheName = "Drivers";
    /** */
    private readonly IIgnite _ignite;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="ignite">Ignite instance.</param>
    public DriversRepository(IIgnite ignite)
    {
        _ignite = ignite ?? throw new ArgumentNullException(nameof(ignite));
    }

    /// <summary>
    /// <inheritdoc cref="DriversRepository"/>
    /// </summary>
    public void Add(Driver driver)
    {
        var cache = _ignite.GetOrCreateCache<Guid, Driver>(CacheName);
        cache[driver.Id] = driver;
    }

    /// <summary>
    /// <inheritdoc cref="DriversRepository"/>
    /// </summary>
    public Driver Get(Guid id)
    {
        var cache = _ignite.GetOrCreateCache<Guid, Driver>(CacheName);
        try
        {
            return cache[id];
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
    }
}
```

First of all, we need to get a reference to a cache object, the simplest way to do it is just create Ignite's `GetOrCreateCache` method.
After that, we can use a general K-V API to store or query the real data.

Let's modify the `Progrma.cs` to verify that it's working:
```csharp
    static void Main(string[] args)
    {
        using var ignite = Ignition.Start();
        IDriversRepository repository = new DriversRepository(ignite);

        Driver driver = GenerateDriver();
        repository.Save(driver);

        Driver persistedDriver = repository.Get(driver.Id);
                
        Console.WriteLine(persistedDriver);
        Debug.Assert(persistedDriver != null);
    }
```

Note, that in general Ignite cache can store any objects, since all it needs is just a plain byte array. And we haven't specified any serialization rules
or additional logic explicitly. The Apache Ignite .NET can serialize our models into byte arrays using reflection. In case if large models with many fields or if there are some intensive computations involved, the default serialization might be a bottleneck. In those cases it's recommended to implement
the `IBinarizable` interface and manually control what fields are being serialized and how. We will check that in upcoming chapters.

### Summary

Now we can install Ignite and use it in our project. We defined our Driver model and can store and retrieve it from a cache. Also we defined the DriverRepository abstraction to decouple a storage model.

In the next chapter we will see how to integrate Apache Ignite with ASP.NET Core Web framework.
