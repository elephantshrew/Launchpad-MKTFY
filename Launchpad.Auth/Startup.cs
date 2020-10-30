using System.Reflection;
using Launchpad.App;
using Launchpad.Auth.Config;
using Launchpad.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Launchpad.Auth
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            //var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql("Host = postgis-db; Port = 5432; Database = devdb; User Id = devdbuser; Password = devdbpassword",
                b =>
                {
                    b.MigrationsAssembly("Launchpad.App");
                })
            );

            services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
            //.AddDefaultTokenProviders();

            var x = services.AddIdentityServer(option =>
            {
                option.IssuerUri = Configuration.GetSection("Identity").GetValue<string>("Authority");
            })
                .AddTestUsers(InMemoryConfig.GetUsers())
                .AddInMemoryClients(InMemoryConfig.GetClients())
                .AddConfigurationStore(opt =>
                {
                    opt.ConfigureDbContext = c =>
                    c.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly("Launchpad.App"));
                })
                .AddOperationalStore(opt =>
                 {
                     opt.ConfigureDbContext = c =>
                     c.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                     sql => sql.MigrationsAssembly("Launchpad.App"));
                 })
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<User>();
                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

        //    app.UseRouting();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapGet("/", async context =>
        //        {
        //            await context.Response.WriteAsync("Hello World!");
        //        });
        //    });
        }
    }
}
