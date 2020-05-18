using System;
using Apache.Ignite.Core;

namespace IgniteTrucksManager.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ignite = Ignition.Start())
            {
                Console.WriteLine("Hello World!");
            }
        }
    }
}
