using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entities;
using Blog.Interfaces;
using Blog.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog
{
    public class Startup
    {
        private IConfigurationRoot _dbConf;

        public Startup(IConfiguration configuration, IHostingEnvironment ENV)
        {
            _dbConf = new ConfigurationBuilder().SetBasePath(ENV.ContentRootPath).AddJsonFile("settings.json").Build();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IBlogRepository, PostRepository>();

            services.AddDbContext<DBContent>(options => options.UseSqlServer(_dbConf.GetConnectionString("DefaultConnection")));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "Getpost",
                    template: "Blog/Post/{id?}", defaults: new { Controller = "Blog", action = "Post" });
            });
            using (var scope = app.ApplicationServices.CreateScope())
            {
                DBContent content = scope.ServiceProvider.GetRequiredService<DBContent>();
                DBObject.Initial(content);
            }
        }
    }
}
