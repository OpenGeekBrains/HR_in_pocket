using HRInPocket.DAL;
using HRInPocket.DAL.Data;
using HRInPocket.Domain;
using HRInPocket.Infrastructure;
using HRInPocket.Infrastructure.Profiles;
using HRInPocket.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Serilog;

namespace HRInPocket
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services
               .AddControllersWithViews()
               .AddRazorRuntimeCompilation();

            services
                .AddDatabase(Configuration)
                .AddIdentity()
                .AddServices();
                
            //services.Configure<RouteOptions>(opt=> 
            //    // ���� � ��������� ����� ������� {type:assignment_type}, �� ������������ ����������� ��������� ����������� ��������
            //    opt.ConstraintMap.Add("assignment_type", typeof(AssignmentTypeConstrain)));

            services.AddAutoMapperWithProfiles(
                typeof(AccountsProfile)
                );

            services.AddSwaggerGen(setup =>
            {
                //setup.OperationFilter<OptionalParameterFilter>(); 
                setup.SwaggerDoc("v1", new OpenApiInfo {Title = "HR in Pocket API", Version = "v1"});
            });
            

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TestDbInitializer db)
        {
            db.Initialize();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                app.UseSwagger();
                app.UseSwaggerUI(setup =>
                    {
                        setup.SwaggerEndpoint("/swagger/v1/swagger.json", "HR in Pocket API v1");
                    }
                );
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ErrorHandkingMiddleware>();
            app.UseMiddleware<TimeLoadMiddleware>();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}