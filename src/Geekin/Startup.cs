﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Geekin.Models;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Geekin
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //Connection strings
            //Senads connenctionstrings
            //var connString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Administrator\Documents\Geekin.mdf; Integrated Security = True; Connect Timeout = 30";
            //Stationär
            //var connString = @"Data Source =.; Initial Catalog = Geekin; Integrated Security = True; Connect Timeout = 15; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            //Laptop
            //var connString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = C:\USERS\ADMINISTRATOR\DOCUMENTS\GEEKIN.MDF; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            //Azure geekin           
            var connString = @"Server = tcp:geekin.database.windows.net,1433; Data Source = geekin.database.windows.net; Initial Catalog = Geekin; Persist Security Info = False; User ID =Senad; Password =Azure89!; Pooling = False; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30; Trusted_Connection = False; Encrypt = True;";
            //Azure User
            var identityConnString = @"Server = tcp:geekinuser.database.windows.net,1433; Data Source = geekinuser.database.windows.net; Initial Catalog = User; Persist Security Info = False; User ID = Senad; Password =Azure89!; Pooling = False; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
            //Alexis connenctionstrings
            //var connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\novas\Documents\geekin.mdf;Integrated Security=True;Connect Timeout=30";

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<DBContext>(o => o.UseSqlServer(connString));

            //Lagra användaren i databasen
            services.AddIdentity<IdentityUser, IdentityRole>(o =>
            {
                //Krav på lösenordet
                o.Password.RequiredLength = 8;
                o.Password.RequireNonLetterOrDigit = false;
                o.Cookies.ApplicationCookie.LoginPath = "/Home/Login";
            })
                .AddEntityFrameworkStores<IdentityDbContext>() //Vart det ska lagras
                .AddDefaultTokenProviders(); //Standard inställingar, regler för lösenord

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<IdentityDbContext>(o => o.UseSqlServer(identityConnString));

            // Kopplat mot DB
            services.AddTransient<IPostsRepository, DbPostsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();
            app.UseStaticFiles();

            app.UseDeveloperExceptionPage();

            app.UseIdentity()
                .UseCookieAuthentication(o =>
            {
                o.AutomaticChallenge = true;
                //o.LoginPath = new PathString("/Members/Login/");
            });

            app.UseMvcWithDefaultRoute();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
