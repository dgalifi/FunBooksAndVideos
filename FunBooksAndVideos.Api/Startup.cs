using System.Collections.Generic;
using FunBooksAndVideos.Domain.Models;
using FunBooksAndVideos.Domain.Rules;
using FunBooksAndVideos.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace FunBooksAndVideos.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IShippingService, ShippingService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ActivateMembershipRule>();
            services.AddSingleton<ProductRule>();

            // The rule engine is flexible
            // In order to add a new Rule, you need to:
            // - create a class derived from IRule
            // - define the behaviour of the Apply method
            // - add the class to the below list

            services.AddSingleton<IOrderService>(
                c => new OrderService(new List<IRule> {

                    // LIST OF RULES TO APPLY
                    c.GetService<ActivateMembershipRule>(),
                    c.GetService<ProductRule>()
            }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
