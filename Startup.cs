using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HelloMVCWorld.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace HelloMVCWorld
{
    public class Startup
    {

        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration; // // Demos How to Fetch values from AppSettings https://asp.mvc-tutorial.com/core-concepts/configuration/
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IOptionsValues>(_configuration.GetSection("IOptionsValues")); // Demos IOptions Dependency Injection https://asp.mvc-tutorial.com/core-concepts/options/

            services.AddSession(options =>  // Maintain  Session  Data https://asp.mvc-tutorial.com/httpcontext/sessions/
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });


            services.AddMvc(); // We need to add MVC support to it, to let the .NET framework and the web server know how to process incoming requests

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , IOptions<IOptionsValues> optionsAccessor)
        {
            string websiteTitle = optionsAccessor.Value.Value1; // optionsAccessor parameter is optional

            if (env.IsDevelopment()) //  Runtime exception  Handling
            {
                app.UseDeveloperExceptionPage(); // In Development Environment , show details of the exception to the developers
            }
            else
            {
                app.UseExceptionHandler("/Error"); // not in Development Environment , show generic information to the user, will route to the error controller
            }

            app.UseSession();  // Maintain  Session  Data https://asp.mvc-tutorial.com/httpcontext/sessions/

            //app.UseStatusCodePagesWithRedirects("/error404.png"); // redirect to errr404.png if runtime HTTP error occurs(not runtime exceptions)
            app.UseStatusCodePagesWithRedirects("/Error/Error404Handling?statusCode={0}"); // Handle error 404 error through  Controller

            app.UseStaticFiles(); // static File in wwwroot Folder can be accessed using /staticFile.jpg

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")),
                RequestPath = "/Images"
            }); // to get list of all files in a particujlar Folder so that User can access them directly (/Images/ )

            // Our web application needs to know how to map incoming requests to your controllers and for this, it uses the concept of routes
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute(); //   Adds endpoints for controller actions to the Microsoft.AspNetCore.Routing.IEndpointRouteBuilder
                                                       //   and adds the default route {controller=Home}/{action=Index}/{id?}.
            });
        }
    }
}
