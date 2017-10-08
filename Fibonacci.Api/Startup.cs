using Fibonacci.Api.Framework;
using Fibonacci.Api.Handlers;
using Fibonacci.Api.Repositories;
using Fibonacci.Messages.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.vNext;

namespace Fibonacci.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IFibRepository, InMemoryFibRepository>();
            ConfigureRabbitMQ(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
        
        private void ConfigureRabbitMQ(IServiceCollection services)
        {
            var options = new RabbitMqOptions();
            var section = Configuration.GetSection("RabbitMQ");
            section.Bind(options);

            var client = BusClientFactory.CreateDefault(options);
            services.AddSingleton<IBusClient>(_ => client);
            services.AddTransient<IEventHandler<ValueCalculated>, ValueCalculatedEventHandler>();
        }

        private void ConfigureRabbitMQSubscriptions(IApplicationBuilder app)
        {
            IBusClient _client = app.ApplicationServices.GetService<IBusClient>();

            var handler = app.ApplicationServices.GetService<IEventHandler<ValueCalculated>>();

            _client.SubscribeAsync<ValueCalculated>(async (msg, context) =>
            {
                await handler.HandleAsync(msg);
            });
        }
    }
}
