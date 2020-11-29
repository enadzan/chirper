using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MassiveJobs.Core;
using MassiveJobs.Core.Hosting;
using MassiveJobs.RabbitMqBroker.Hosting;

using Chirper.Server.DomainModel;
using Chirper.Server.Infrastructure;
using Chirper.Server.Infrastructure.Identity;
using Chirper.Server.Jobs;
using Chirper.Server.Repositories;

namespace Chirper.Server
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
            services.AddDbContextPool<EF.ChirpDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ChirpDb"));
            });

            services.AddIdentity<ChirpUser, ChirperIdentityRole>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer() 
                .AddApiAuthorization<ChirpUser, EF.ChirpDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddTransient<IUserStore<ChirpUser>, ChirperUserStore>();
            services.AddTransient<IUserPasswordStore<ChirpUser>, ChirperUserStore>();
            services.AddTransient<IUserSecurityStampStore<ChirpUser>, ChirperUserStore>();
            services.AddTransient<IRoleStore<ChirperIdentityRole>, ChirperRoleStore>();

            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

            services.AddScoped<IChirpDb, EF.Repositories.ChirpDb>();

            // Using a custom job type provider is optional - here it is used to shorten the tag names.
            //
            // The DefaultTypeProvider uses complete class names. It's good to get something running quickly,
            // but makes refactoring (renaming classes) more difficult.
            services.AddSingleton<IJobTypeProvider, CustomJobTypeProvider>();

            services.AddMassiveJobs(o =>
                {
                    o.MassiveJobsSettings = new MassiveJobsSettings(
                        Configuration.GetValue<string>("MassiveJobs:NamePrefix")
                    );
                    o.OnInitAction = () =>
                    {
                        TestPeriodicJob.PublishPeriodic("test_periodic", 10);
                    };
                })
                .UseRabbitMqBroker(s =>
                {
                    s.HostNames = new[] {Configuration["MassiveJobs:RabbitMq:Hostname"]};
                    s.Username = Configuration.GetValue<string>("MassiveJobs:RabbitMq:Username");
                    s.Password = Configuration.GetValue<string>("MassiveJobs:RabbitMq:Password");
                    s.Port = Configuration.GetValue<int>("MassiveJobs:RabbitMq:Port");
                    s.VirtualHost = Configuration.GetValue<string>("MassiveJobs:RabbitMq:VirtualHost");
                });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
