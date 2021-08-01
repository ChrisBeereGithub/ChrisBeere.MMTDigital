using ChrisBeere.MMTDigital.WebApi.Data.DatabaseContext;
using ChrisBeere.MMTDigital.WebApi.Data.Repositories.Implementations;
using ChrisBeere.MMTDigital.WebApi.Data.Repositories.Interfaces;
using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Implementations;
using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Interfaces;
using ChrisBeere.MMTDigital.WebApi.Services.Implementations;
using ChrisBeere.MMTDigital.WebApi.Services.Interfaces;
using ChrisBeere.MMTDigital.WebApi.Validation.Implementations;
using ChrisBeere.MMTDigital.WebApi.Validation.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using System;
using System.IO;
using System.Net.Http;

namespace ChrisBeere.MMTDigital.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();

            services.AddControllers();

            // Register database context with dependency resolver.
            // EnableRetryOnFailure mitigates the Sql Server not being available owing to transient errors.
            // The SSE_Test database cold starts frequently. 
            services.AddDbContext<SSETestContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SSETestDatabase")
                                    ,providerOptions => providerOptions.EnableRetryOnFailure()));

            // Setup Polly for Customer Web Api.
            services.AddHttpClient("CustomerApi").AddPolicyHandler(GetRetryPolicy());

            // Register request lifetime components with dependency resolver.
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRequestValidator, CustomerRequestValidator>();

            // HttpClient is intended to be instantiated once and re-used throughout the life of an application.
            services.AddSingleton<IHttpClientService, HttpClientService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChrisBeere.MMTDigital API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                // Handle HttpRequestExceptions, 408 and 5xx status codes
                .HandleTransientHttpError()
                // Handle 404 not found
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                // Handle 401 Unauthorized
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                // What to do if any of the above errors occur:
                // Retry 3 times, each time wait 1,2 and 4 seconds before retrying.
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
