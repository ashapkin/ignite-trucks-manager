using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Apache.Ignite.Core;
using Apache.Ignite.Core.Discovery.Tcp;
using Apache.Ignite.Core.Discovery.Tcp.Static;
using Apache.Ignite.Core.Failure;
using Apache.Ignite.Core.Log;
using NUnit.Framework;

namespace IgniteRoute.Core.Tests.Utils
{
    /// <summary>
    /// Test utils.
    /// </summary>
    internal static class TestUtils
    {
        private static readonly string WorkDir =
            // ReSharper disable once AssignNullToNotNullAttribute
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ignite_work");

        /// <summary>
        /// Gets the default code-based test configuration.
        /// </summary>
        public static IgniteConfiguration GetTestConfiguration(string name = null)
        {
            return new IgniteConfiguration
            {
                DiscoverySpi = GetStaticDiscovery(),
                Localhost = "127.0.0.1",
                IgniteInstanceName = name,
                FailureHandler = new NoOpFailureHandler(),
                WorkDirectory = WorkDir,
                Logger = new TestContextLogger()
            };
        }

        /// <summary>
        /// Gets the static discovery.
        /// </summary>
        public static TcpDiscoverySpi GetStaticDiscovery()
        {
            return new TcpDiscoverySpi
            {
                IpFinder = new TcpDiscoveryStaticIpFinder
                {
                    Endpoints = new[] { "127.0.0.1:47500" }
                },
                SocketTimeout = TimeSpan.FromSeconds(0.3)
            };
        }

        /// <summary>
        /// Logs to test progress. Produces immediate console output on .NET Core.
        /// </summary>
        public sealed class TestContextLogger : ILogger
        {
            /** <inheritdoc /> */
            public void Log(LogLevel level, string message, object[] args, IFormatProvider formatProvider,
                string category, string nativeErrorInfo, Exception ex)
            {
                if (!IsEnabled(level))
                {
                    return;
                }

                var text = args != null
                    ? string.Format(formatProvider ?? CultureInfo.InvariantCulture, message, args)
                    : message;

                TestContext.Progress.WriteLine(text);
            }

            /** <inheritdoc /> */
            public bool IsEnabled(LogLevel level)
            {
                return level >= LogLevel.Info;
            }
        }
    }
}
