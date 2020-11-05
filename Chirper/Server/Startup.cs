using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.AddTransient<IUserStore<ChirpUser>, ChirperUserStore>();
            services.AddTransient<IUserPasswordStore<ChirpUser>, ChirperUserStore>();
            services.AddTransient<IUserSecurityStampStore<ChirpUser>, ChirperUserStore>();
            services.AddTransient<IRoleStore<ChirperIdentityRole>, ChirperRoleStore>();

            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

            services.AddScoped<IChirpDb, EF.Repositories.ChirpDb>();

            services.AddMassiveJobs(options =>
            {
                options.RabbitMqSettings.HostNames = new[] {Configuration["RabbitMq:Hostname"]};
                options.RabbitMqSettings.Username = Configuration.GetValue<string>("RabbitMq:Username");
                options.RabbitMqSettings.Password = Configuration.GetValue<string>("RabbitMq:Password");
                options.RabbitMqSettings.Port = Configuration.GetValue<int>("RabbitMq:Port");
                options.RabbitMqSettings.VirtualHost = Configuration.GetValue<string>("RabbitMq:VirtualHost");
                options.RabbitMqSettings.NamePrefix = Configuration.GetValue<string>("RabbitMq:NamePrefix");

                // Using a custom job type provider is optional - here it is used to shorten the tag names.
                //
                // The DefaultTypeProvider uses complete class names. It's good to get something running quickly,
                // but makes refactoring (renaming classes) more difficult.

                options.JobTypeProvider = new CustomJobTypeProvider();
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
