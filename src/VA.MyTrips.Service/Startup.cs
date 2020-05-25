using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using VA.MyTrips.Business.Components.Definition;
using VA.MyTrips.Business.Components.Implementation;
using VA.MyTrips.Data.Repositories.Definition;
using VA.MyTrips.Data.Repositories.Implementation;

namespace VA.MyTrips.Service
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddAutoMapper(typeof(AutoMapper.MappingProfile));
            services.AddSingleton<ITripComponent, TripComponent>();
            services.AddSingleton(GetCosmosClientInstance());
            services.AddSingleton(GetBlobServiceClient());
            services.AddSingleton<ITripRepository, TripRepository>();
            services.AddSingleton<IPhotoRepository, PhotoRepository>();
            services.AddSingleton<IPhotoStorageRepository, PhotoStorageRepository>();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<TripService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }

        private static BlobServiceClient GetBlobServiceClient() 
        {

            string connectionString = Environment.GetEnvironmentVariable("STORAGE_ACCOUNT_CONNECTION_STRING");
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            return blobServiceClient;

        }

        /// Creates a Cosmos DB database and a container with the specified partition key. 
        private static CosmosClient GetCosmosClientInstance()
        {
            string account = Environment.GetEnvironmentVariable("COSMOS_DB");
            string key = Environment.GetEnvironmentVariable("COSMOS_DB_KEY");
            Microsoft.Azure.Cosmos.Fluent.CosmosClientBuilder clientBuilder = new Microsoft.Azure.Cosmos.Fluent.CosmosClientBuilder(account, key);
            return clientBuilder
                                .WithConnectionModeDirect()
                                .Build();
        }

    }
}
