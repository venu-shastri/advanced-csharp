using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.Use(async (context, next) =>
            {
                // Do work that doesn't write to the Response.
                await context.Response.WriteAsync("Authentication Middleware!");
             
            });
            app.Use(async (context, next) =>
            {
                // Do work that doesn't write to the Response.
                await context.Response.WriteAsync("Session  Middleware!");

            });
            app.Use(async (context, next) =>
            {
                // Do work that doesn't write to the Response.
                await context.Response.WriteAsync("Authorization  Middleware!");

            });
            app.Use(async (context, next) =>
            {
                // Do work that doesn't write to the Response.
                await context.Response.WriteAsync("Cache  Middleware!");

            });
            app.Use(async (context, next) =>
            {
                // Do work that doesn't write to the Response.
                await context.Response.WriteAsync("Exception Middleware!");

            });


            app.Run(async context => await context.Response.WriteAsync("End Point Middleware!"));



        }

        
    }
}
