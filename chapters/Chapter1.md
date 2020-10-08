# Chapter 1

### Servers and clients.

Apache Ignite has a notion of server and client nodes and a cluster should have at least one server node. A client node at the same time could be either a thick node or a thin node. The main difference between them is the fact that a thick node is considered as a part of a cluster and participates in some cluster-wide operations like Partition Map Exchange, shares the same communication and discovery protocol, etc. Roughly speaking it is almost a server node but it can't store the data. Contrary, a thin client is a recent feature and it behaves just like a database driver, it communicates with the cluster through internal TCP protocol and is stateless.

### Supported languages.

Apache Ignite is a multiplatform system and it supports different languages, such as Java, C#, C++, Python, and others. But a server or thick clients are supported only for Java, .NET and C++ platforms. The thin clients are lightweight and could be implemented in any language, cause the communication protocol is platform-independent. The .NET and C++ versions are not written from scratch, instead, they utilize existing Java code and call required function thought the JNI system calls. Therefore it's mandatory to install some kind of JRE, please, refer to the [official docs](https://apacheignite-net.readme.io/docs/troubleshooting) for more details. Most of the common pitfalls are describe at the [troubleshooting docs](https://apacheignite-net.readme.io/docs/troubleshooting) as well.

### Apache Ignite installation.

After we have JRE installed, let's start creating a new .NET Core 3.1 console project - `IgiteRoute.Core` and installing the following Nuget package:
```xml
<ItemGroup>
  <PackageReference Include="Apache.Ignite" Version="2.8.0" />
</ItemGroup>
```

Note, that after the installation, there are should be two folders within your project: `config` with the default java logging properties and `libs` with the java packages. We don't need to specify `IGNITE_HOME` as well unless you have placed your `libs` folder outside of the project. 

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

Now it's time to define our business models. We will start with the simple `Driver.cs` model and add some fields to it.

```csharp
/// <summary>
/// Driver model.
/// </summary>
public class Driver
{
    /// <summary>
    /// Identifier.
    /// </summary>
    public Guid Id { get; set; }
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

To work with our `Driver` we need the `IDriversRepository` abstraction and `DriversRepository` that will work with Apache Ignite directly. 
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
    /// <inheritdoc cref="IDriversRepository"/>
    /// </summary>
    public void Add(Driver driver)
    {
        var cache = _ignite.GetOrCreateCache<Guid, Driver>(CacheName);
        cache[driver.Id] = driver;
    }

    /// <summary>
    /// <inheritdoc cref="IDriversRepository"/>
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

In Apache Ignite everything is stored within a cache instance. You might think of it as a table in RDBS for now. Once created, we need to obtain a reference to it, we can join two steps and use a single `GetOrCreateCache` method. The `ICache ` interface is quite similar to the regular `IDictionary` key-value object and the API is intuitive. 

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

The output:
```
...
Driver [John - 05389e44-bc05-4c4b-b957-d33da4dc2f81]
...
```

Note, that in general Ignite cache can store any objects, since all it needs is just a plain byte array also we haven't specified any serialization rules
or additional logic explicitly. The Apache Ignite .NET can serialize our models into byte arrays using reflection. For large models with many fields or if there are some intensive computations involved, the default serialization might be a bottleneck. In this case, it's recommended to implement the `IBinarizable` interface and manually control what fields are being serialized and how. We will check that in upcoming chapters.

### Summary

In this chapter, we installed Java runtime and Apache Ignite .NET. We started our application by defining a simple `Driver` model and `IDriversRepository` for data modification. Also, we verified our installation with some hardcoded data running a simple console application. In the next chapter, we will add Web API support for our project.
