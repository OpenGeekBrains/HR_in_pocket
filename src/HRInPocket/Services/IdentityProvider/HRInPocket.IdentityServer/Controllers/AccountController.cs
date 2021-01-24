using System;
using System.Linq;
using System.Threading.Tasks;
using HRInPocket.IdentityServer.Controllers.Helpers;
using HRInPocket.IdentityServer.Extensions;
using HRInPocket.IdentityServer.InMemoryConfig;
using HRInPocket.IdentityServer.Models;
using HRInPocket.IdentityServer.ViewModels;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.IdentityServer.Controllers
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly IIdentityServerInteractionService _Interaction;
        private readonly IClientStore _ClientStore;
        private readonly IAuthenticationSchemeProvider _SchemeProvider;
        private readonly IEventService _Events;

        public AccountController(
            UserManager<ApplicationUser> UserManager,
            SignInManager<ApplicationUser> SignInManager,
            IIdentityServerInteractionService Interaction,
            IClientStore ClientStore,
            IAuthenticationSchemeProvider SchemeProvider,
            IEventService events)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Interaction = Interaction;
            _ClientStore = ClientStore;
            _SchemeProvider = SchemeProvider;
            _Events = events;
        }

        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string ReturnUrl)
        {
            // построение вью-модели входа
            var vm = await BuildLoginViewModelAsync(ReturnUrl);

            if (vm.IsExternalLoginOnly)
            {
                // если разрешена только авторизация через внешних поставщиков
                return RedirectToAction("Challenge", "External", new { provider = vm.ExternalLoginScheme, ReturnUrl });
            }

            return View(vm);
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model, string button)
        {
            // Формируем контекст запроса авторизации
            var context = await _Interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            // пользователь нажал кнопку "login"
            if (button == "login")
            {
                if (ModelState.IsValid)
                {
                    var result = await _SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        var user = await _UserManager.FindByNameAsync(model.Username);
                        await _Events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id.ToString(), user.UserName));

                        if (context != null)
                        {
                            if (await _ClientStore.IsPkceClientAsync(context.Client.ClientId))
                            {
                                //для поддержки PKCE
                                return View("Redirect", new RedirectViewModel {RedirectUrl = model.ReturnUrl});
                            }

                            //возвращаемся туда откуда был запрос авторизации
                            return Redirect(model.ReturnUrl);
                        }

                        // если запрос локальный
                        if (Url.IsLocalUrl(model.ReturnUrl))
                        {
                            //возвращаемся по ссылке откуда пришли
                            return Redirect(model.ReturnUrl);
                        }
                        // если нет ссылкы для редиректа
                        if (string.IsNullOrEmpty(model.ReturnUrl))
                        {
                            //возвращаемся на дмашнюю страницу сервера
                            return Redirect("~/");
                        }

                        // во всех остальных случаях - ссылка для редиректа не корректна (или подделана / скомпрометирована и т.д.)
                        throw new Exception("invalid return URL");
                    }

                    // формируем список ошибок входа
                    await _Events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials"));
                    // добавляем ошибки валидации в состояние модели
                    ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
                }

                // показываем форму с ошибками
                var vm = await BuildLoginViewModelAsync(model);
                return View(vm);
            }

            // нажали на кнопку "register"
            if (button == "register")
            {
                // получаем текующу ссылку для редиректа
                var ReturnUrl = model.ReturnUrl;
                
                // переходим в контроллер регистрации с передачей ссылки редиректа
                return RedirectToAction("Register", "Register", new { ReturnUrl });
            }

            // нажали "cansel"
            if (context != null)
            {
                // отказываем в доступе
                await _Interaction.GrantConsentAsync(context, new ConsentResponse { Error = AuthorizationError.AccessDenied });

                // для поддержки PKCE
                if (await _ClientStore.IsPkceClientAsync(context.Client.ClientId))
                {
                    return View("Redirect", new RedirectViewModel { RedirectUrl = model.ReturnUrl });
                }

                // возвращаемся по ссылке редиректа (с ошибкой 401 т.к. был вызов Interaction.GrantConsent c передачей ошибки авторизации )
                return Redirect(model.ReturnUrl);
            }

            // Возврат на домашнюю страницу сервера (поддержка автономной работы сервера и входа без внешнего запроса)
            return Redirect("~/");
        }


        /// <summary>
        /// Show logout page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string LogoutId)
        {
            // построение модели
            var vm = await BuildLogoutViewModelAsync(LogoutId);

            // если включен переход на страницу предупреждения о выходе
            if (vm.ShowLogoutPrompt) return View(vm);
            
            // просто выходим
            return await Logout(vm);

        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            // построение вью-модели
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            // если пользователь авторизован
            if (User?.Identity.IsAuthenticated == true)
            {
                // удаляем локальные куки
                await _SignInManager.SignOutAsync();

                // вызов выхода на сервере
                await _Events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            // если через наш сервер никуда не входили
            if (!vm.TriggerExternalSignout)
            {
                if(!vm.AutomaticRedirectAfterSignOut) return View("LoggedOut", vm);
                return Redirect(vm.PostLogoutRedirectUri);
            }
            
            //создаем ссылку на возврат после выхода из других серверов
            var url = Url.Action("Logout", new { logoutId = vm.LogoutId });

            // выходим из других мест
            return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
        }

        [HttpGet]
        public IActionResult AccessDenied() => View();


        /*****************************************/
        /* helper APIs for the AccountController */
        /*****************************************/
        private async Task<LoginViewModel> BuildLoginViewModelAsync(string ReturnUrl)
        {
            var context = await _Interaction.GetAuthorizationContextAsync(ReturnUrl);
            if (context?.IdP != null)
            {
                // this is meant to short circuit the UI and only trigger the one external IdP
                return new LoginViewModel
                {
                    EnableLocalLogin = false,
                    ReturnUrl = ReturnUrl,
                    Username = context.LoginHint,
                    ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } }
                };
            }

            var schemes = await _SchemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null ||
                            (x.Name.Equals(AccountOptions.WindowsAuthenticationSchemeName, StringComparison.OrdinalIgnoreCase))
                )
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allow_local = true;
            if (context?.Client.ClientId != null)
            {
                var client = await _ClientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
                if (client != null)
                {
                    allow_local = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allow_local && AccountOptions.AllowLocalLogin,
                ReturnUrl = ReturnUrl,
                Username = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Username = model.Username;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string LogoutId)
        {
            var vm = new LogoutViewModel { LogoutId = LogoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            if (User?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _Interaction.GetLogoutContextAsync(LogoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string LogoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _Interaction.GetLogoutContextAsync(LogoutId);

            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = LogoutId
            };

            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var provider_supports_signout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (provider_supports_signout)
                    {
                        if (vm.LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we signout and redirect away to the external IdP for signout
                            vm.LogoutId = await _Interaction.CreateLogoutContextAsync();
                        }

                        vm.ExternalAuthenticationScheme = idp;
                    }
                }
            }

            return vm;
        }
    }
}
