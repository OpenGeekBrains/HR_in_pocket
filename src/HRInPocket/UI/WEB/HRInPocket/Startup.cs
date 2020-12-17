using System.IdentityModel.Tokens.Jwt;
using HRInPocket.DAL;
using HRInPocket.DAL.Data;
using HRInPocket.Infrastructure;
using HRInPocket.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace HRInPocket
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Стандартный протокол oidc, с индивидуальной настройкой
            #region OpenId Connect
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = "Cookies";
                opt.DefaultChallengeScheme = "oidc";
            })
               .AddCookie("Cookies", opt =>
                {
                    opt.AccessDeniedPath = "/Account/AccessDenied"; // указать, если действие в другом контроллере
                })
               .AddOpenIdConnect("oidc", opt =>
                    {
                        opt.SignInScheme = "Cookies";
                        opt.Authority = "https://localhost:10001";
                        opt.ClientId = "HRInPocket-WebClient-MVC";
                        opt.ResponseType = "code id_token";
                        opt.SaveTokens = true;
                        opt.ClientSecret = Configuration["OpenIdConnect:ClientSecret"];
                        opt.GetClaimsFromUserInfoEndpoint = true; //добавлять IdentityClaim в AccessToken

                        //удаление клаймов
                        opt.ClaimActions.DeleteClaim("sid");
                        opt.ClaimActions.DeleteClaim("idp");

                        //добавление клаймов
                        opt.ClaimActions.MapUniqueJsonKey("role", "role"); // старые добрые роли IdentityRole
                        opt.ClaimActions.MapUniqueJsonKey("position", "position"); // Можно контролировать доступ юзеров по конкретным городам
                        opt.ClaimActions.MapUniqueJsonKey("country", "country"); // ... или странам

                        //добавление скопов 
                        opt.Scope.Add("email");
                        opt.Scope.Add("address");
                        opt.Scope.Add("roles");
                        //opt.Scope.Add("weatherApi"); // тот самый API для примера, которого нет
                        opt.Scope.Add("position"); // город
                        opt.Scope.Add("country"); // страна
                        //... и т.д.

                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            RoleClaimType = "role" //параматр для валидации по клаймам ролей
                        };
                    });
            #endregion

            #region Авторизация
            services.AddAuthorization(AuthOpt =>
                {
                    AuthOpt.AddPolicy("CanCreateAndModifyData", PolicyBuilder =>
                    {
                        PolicyBuilder.RequireAuthenticatedUser();
                        PolicyBuilder.RequireClaim("position", "Administrator");
                        PolicyBuilder.RequireClaim("country", "Russia");
                    });
                }); 
            #endregion

            services
               .AddControllersWithViews()
               .AddRazorRuntimeCompilation();

            services
               .AddDatabase(Configuration)
               .AddServices();

            //services.AddAutoMapperWithProfiles(
            //    typeof(AccountsProfile)
            //    );
            //services.AddSwaggerGen(setup =>
            //{
            //    //setup.OperationFilter<OptionalParameterFilter>(); 
            //    setup.SwaggerDoc("v1", new OpenApiInfo {Title = "HR in Pocket API", Version = "v1"});
            //});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TestDbInitializer db)
        {
            db.Initialize();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                //app.UseSwagger();
                //app.UseSwaggerUI(setup =>
                //    {
                //        setup.SwaggerEndpoint("/swagger/v1/swagger.json", "HR in Pocket API v1");
                //    }
                //);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
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