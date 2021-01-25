using AO.ActionFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp
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
            // this is if you have classic MVC controllers + views
            services.AddMvc(setup => AddFilter(setup, (allowedKeys) => new KeyActionFilter(allowedKeys)));

            // for razor pages
            services
                .AddRazorPages()
                .AddMvcOptions(setup => AddFilter(setup, (allowedKeys) => new KeyPageFilter(allowedKeys)));

            // local function serving both Page and MVC scenarios
            void AddFilter<TFilter>(MvcOptions setup, Func<HashSet<string>, TFilter> factory) where TFilter : IFilterMetadata
            {
                var allowedKeys = Configuration.GetSection("AllowedKeys")
                    .AsEnumerable()
                    .Select(kp => kp.Value)
                    .Where(value => !string.IsNullOrEmpty(value))
                    .ToHashSet();

                setup.Filters.Add(factory.Invoke(allowedKeys));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
