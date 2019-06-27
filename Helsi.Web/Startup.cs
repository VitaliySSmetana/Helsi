using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helsi.DataAccess;
using Helsi.DataAccess.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Helsi.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<HelsiContext>(opt => opt.UseInMemoryDatabase("HelsiDB"));

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

            AddSeeds(app);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void AddSeeds(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<HelsiContext>();

                context.Genders.AddRange(
                    new Gender { Id = 1, Name = "Male" },
                    new Gender { Id = 2, Name = "Female" },
                    new Gender { Id = 3, Name = "Other" }
                );

                context.ContactTypes.AddRange(
                    new ContactType { Id = 1, Name = "Work" },
                    new ContactType { Id = 2, Name = "Home" },
                    new ContactType { Id = 3, Name = "Emergency" }
                );

                context.Patients.AddRange(
                    new Patient { Id = 1, GenderId = 1, FirstName = "John", LastName = "Doe", Patronymic = "Nillson", PhoneNumber = "+380551012233", BirthDate = new DateTime(2000, 12, 23) },
                    new Patient { Id = 2, GenderId = 3, FirstName = "Abigail", LastName = "Black", Patronymic = "Jackson", PhoneNumber = "+380663013255", BirthDate = new DateTime(2005, 1, 16) },
                    new Patient { Id = 3, GenderId = 2, FirstName = "Ann", LastName = "Smith", Patronymic = "Willson", PhoneNumber = "+380681238899", BirthDate = new DateTime(2003, 2, 1) }
                );

                context.AdditionalContacts.AddRange(
                    new AdditionalContact { Id = 1, PatientId = 1, ContactTypeId = 3, PhoneNumber = "+380668005003" },
                    new AdditionalContact { Id = 2, PatientId = 1, ContactTypeId = 2, PhoneNumber = "+380667006002" },
                    new AdditionalContact { Id = 3, PatientId = 3, ContactTypeId = 1, PhoneNumber = "+380655009007" },
                    new AdditionalContact { Id = 4, PatientId = 2, ContactTypeId = 2, PhoneNumber = "+380675806033" }
                );

                context.SaveChanges();
            }
        }
    }
}
