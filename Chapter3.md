# Chapter 3

### Configuring Ignite logging for .NET Core

Logging is important whenever you are trying to understand what's going on with your grid. 
It's helpful to debug your application or detect edge cases.

Since Ignite is internally written in Java, then the default logging also happens withing JVM process.
If you download a binary distributive, then you will notice that there is a file called `/config/java.util.logging.properties`.
Thus, the simplest way to enable logging is to edit the `java.util.logging.properties` or add it to the classpath if it wasn't added previously.

Ignite .NET knows how to deal with Ignite logging and forward the Java logs to the .NET world with help of JNI framework.
You can check more details in the official docs https://apacheignite-net.readme.io/docs/logging

#### Configuring log4Net to work with Apache Ignite.

In our example, we are using the `Log4Net` framework. In order to start using it, the following packages need to be installed:

1. Install packages

```
<PackageReference Include="Apache.Ignite.Log4Net" Version="2.8.0" />
<PackageReference Include="log4net" Version="2.0.8" />
```

2. Configure Log4Net template

Let's create a Log4Net config file `config/log4net.xml`:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<log4net>
  <appender name="ConsoleAppender" type="log4net.Appender.ConsoleAppender" >
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%date [%thread] %-5level %logger [%ndc] - %message%newline" />
    </layout>
  </appender>
  <appender name="FileAppender" type="log4net.Appender.RollingFileAppender">
    <file value="ignite/work/log/ignite.log" />
    <appendToFile value="true" />
    <rollingStyle value="Size" />
    <maxSizeRollBackups value="10" />
    <maximumFileSize value="100KB" />
    <staticLogFileName value="true" />
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%date [%thread] %-5level %logger [%property{NDC}] - %message%newline" />
    </layout>
  </appender>
  <root>
    <level value="INFO" />
    <appender-ref ref="ConsoleAppender" />
    <appender-ref ref="FileAppender" />
  </root>
</log4net>
```

3. Add Ignite logger configuration

The configuration is straightforward, check out the log4net docs for more details.

The last thing to do is to tell Ignite that we want to use the configured logger:
```xml
<igniteConfiguration xmlns="http://ignite.apache.org/schema/dotnet/IgniteConfigurationSection" gridName="ignite-example">
  ...

  <logger type="Apache.Ignite.Log4Net.IgniteLog4NetLogger, Apache.Ignite.Log4Net" />
  
  ...

</igniteConfiguration>
```

4. Configure ASP.NET Core logger factory.

And configure the ASP.NET logging service:
```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
{
  ...
   
  loggerFactory.AddLog4Net("config/log4net.xml");

  ...
        
```

5. Using in code

```csharp
[HttpGet]
        [Route("topdrivers")]
        public object TopDriversSql()
        {
            _logger.LogDebug("Getting top drivers");

            var sql = "select Name, Rating from Driver order by Rating desc";

            return _repository.Query(sql);
        }
```

6. Checking result

...

Now the logs should be written to the `ignite/work/log/` directory of `IgniteTrucksManager.Api` project

#### Summary

Now we know how to enable Apache Ignite logging using log4Net. You can check another examples of other log integrations in official docks LINK TO DOCS. 
It's impoerant to have logs if you want to ask a question about Ignite at the forum or SO...

In the next chapter we will see how to work with SQL from .NET side.
