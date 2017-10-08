using Fibonacci.Messages.Commands;
using Fibonacci.Service.Framework;
using Fibonacci.Service.Handlers;
using Fibonacci.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.vNext;

namespace Fibonacci.Service
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
            ConfigureRabbitMQSubscriptions(app);
        }

        private void ConfigureRabbitMQ(IServiceCollection services)
        {
            var options = new RabbitMqOptions();
            var section = Configuration.GetSection("RabbitMQ");
            section.Bind(options);

            var client = BusClientFactory.CreateDefault(options);
            services.AddSingleton<IBusClient>(_ => client);
            services.AddSingleton<ICalc>(_ => new Calc());
            services.AddTransient<ICommandHandler<CalculateValue>, CalculateValueHandler>();
        }

        private void ConfigureRabbitMQSubscriptions(IApplicationBuilder app)
        {
            IBusClient _client = app.ApplicationServices.GetService<IBusClient>();

            var handler = app.ApplicationServices.GetService<ICommandHandler<CalculateValue>>();

            _client.SubscribeAsync<CalculateValue>(async (msg, context) =>
            {
                await handler.HandleAsync(msg);
            });
        }
    }
}
