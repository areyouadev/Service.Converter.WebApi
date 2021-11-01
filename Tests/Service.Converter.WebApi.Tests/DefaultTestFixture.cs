using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Service.Converter.WebApi.Tests
{
    using Xunit;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    public class DefaultTestFixture : WebApplicationFactory<Startup>
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(IHealthCheck));

                /*
                // get reference database dependency 
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<PrimaryContext>));
             
                // remove relational database 
                services.Remove(descriptor);

                services.Where(s => s.ImplementationType == typeof(DatabaseInitializerService))
                    .ToList()
                    .ForEach(service => services.Remove(service));

                // add in memory database 
                services.AddDbContext<PrimaryContext>(options =>
                {
                    options.UseInMemoryDatabase($"InMemoryDbForTesting{Guid.NewGuid().ToString("N")}");

                });

                services.AddSingleton<IHostedService, DatabaseInitializerService>();
                */
            });
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(x => { x.UseStartup<Startup>(); })
                .ConfigureAppConfiguration((_, configurationBuilder) =>
                {

                });
            return builder;
        }
    }

    [CollectionDefinition(nameof(DefaultTestFixtureCollection))]
    public class DefaultTestFixtureCollection : ICollectionFixture<DefaultTestFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

   

}
