using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;

namespace HRInPocket.IdentityServer.InMemoryConfig
{
    public class DefaultConfig
    {
        private readonly IConfiguration _Configuration;

        public DefaultConfig(IConfiguration configuration) => _Configuration = configuration;

        /// <summary>
        /// Ресурсы пользователей
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "User role(s)", new List<string> { "role" }),
                new IdentityResource("position", "Your position", new List<string> { "position" }),
                new IdentityResource("country", "Your country", new List<string> { "country" })
            };

        /// <summary>
        /// Создает и возвращает колеекцию базовых пользователей
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers() =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
                    Username = "Admin",
                    Password = "AdminPassword",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Administrator"),
                        new Claim("family_name", "Default"),
                        new Claim("email", "DefaultAdmin@mail.com"),
                        new Claim("address", "Moskow, Kremlin"),
                        new Claim("role", "Admin"),
                        new Claim("position", "Administrator"),
                        new Claim("country", "Russia")
                    }
                },
                new TestUser
                {
                    SubjectId = "c88f9eab-2dc9-48fb-8ab8-74e1febedb21",
                    Username = "TestManager",
                    Password = "Password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Manager"),
                        new Claim("family_name", "Default"),
                        new Claim("email", "TestManager@mail.com"),
                        new Claim("address", "Moskow, Kremlin"),
                        new Claim("role", "Manager"),
                        new Claim("position", "Manager"),
                        new Claim("country", "Russia")
                    }
                },
                new TestUser
                {
                    SubjectId = "c95ddb8c-79ec-488a-a485-fe57a1462340",
                    Username = "TestApplicant",
                    Password = "Password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Applicant"),
                        new Claim("family_name", "Default"),
                        new Claim("email", "TestApplicant@mail.com"),
                        new Claim("address", "Moskow, Kremlin"),
                        new Claim("role", "Applicant"),
                        new Claim("position", "Applicant"),
                        new Claim("country", "Russia")
                    }
                }
            };

        /// <summary>
        /// Создает и возвращает коллекцию зарегистрированных клиентов сервера
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                //Основной клиент - MVC-приложение
                new Client
                {
                    ClientName = "HRInPocket WebClient MVC",
                    ClientId = "HRInPocket-WebClient-MVC",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new List<string>{ "https://localhost:5001/signin-oidc" }, //адрес клиента + /signin-oidc 
                    ClientSecrets = { new Secret(_Configuration["OpenIdConnect:HRInPocket-WebClient-MVC-Secret"].Sha512()) },
                    PostLogoutRedirectUris = new List<string> { "https://localhost:5001/signout-callback-oidc" }, //адрес клиента + /signout-callback-oidc 
                    RequireConsent = false, //не показывать страницу согласия на передачу данных между сервером и клиентом
                    RequirePkce = false,
                    AllowedScopes = 
                    { 
                        //здесь добавляются разрешенные для клиента области
                        //то, к чему клиент сможет получать доступ
                        IdentityServerConstants.StandardScopes.OpenId, 
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        //"weatherApi", // это скоп указан для примера.
                        "position",
                        "country"
                    }
                }
            };

        /// <summary>
        /// Список микросервисов, к которым также может быть предоставлен доступ через сервер
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope> { new ApiScope("weatherApi", "Weather Forecast API") };// для примера указан API

        /// <summary>
        /// Список API ресурсов, к которым можно предоставлять доступ
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("weatherApi", "Weather Forecast API") // для примера
                {
                    Scopes = { "weatherApi" }
                }
            };
    }
}
