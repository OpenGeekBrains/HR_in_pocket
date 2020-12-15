using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRInPocket.IdentityServer.Controllers.Helpers;
using HRInPocket.IdentityServer.InMemoryConfig;
using HRInPocket.IdentityServer.Models;
using HRInPocket.IdentityServer.ViewModels;
using IdentityServer4.Configuration;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HRInPocket.IdentityServer.Controllers
{
    [Authorize]
    [SecurityHeaders]
    public class DeviceController : Controller
    {
        private readonly IDeviceFlowInteractionService _Interaction;
        private readonly IEventService _Events;
        private readonly IOptions<IdentityServerOptions> _Options;
        private readonly ILogger<DeviceController> _Logger;

        public DeviceController(
            IDeviceFlowInteractionService Interaction,
            IEventService EventService,
            IOptions<IdentityServerOptions> Options,
            ILogger<DeviceController> Logger)
        {
            _Interaction = Interaction;
            _Events = EventService;
            _Options = Options;
            _Logger = Logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user_code_param_name = _Options.Value.UserInteraction.DeviceVerificationUserCodeParameter;
            var user_code = Request.Query[user_code_param_name];
            if (string.IsNullOrWhiteSpace(user_code)) return View("UserCodeCapture");

            var vm = await BuildViewModelAsync(user_code);
            if (vm == null) return View("Error");

            vm.ConfirmUserCode = true;
            return View("UserCodeConfirmation", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCodeCapture(string UserCode)
        {
            var vm = await BuildViewModelAsync(UserCode);
            if (vm == null) return View("Error");

            return View("UserCodeConfirmation", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Callback(DeviceAuthorizationInputModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var result = await ProcessConsent(model);
            if (result.HasValidationError) return View("Error");

            return View("Success");
        }

        private async Task<ProcessConsentResult> ProcessConsent(DeviceAuthorizationInputModel model)
        {
            var result = new ProcessConsentResult();

            var request = await _Interaction.GetAuthorizationContextAsync(model.UserCode);
            if (request == null) return result;

            ConsentResponse granted_consent = null;

            // user clicked 'no' - send back the standard 'access_denied' response
            if (model.Button == "no")
            {
                granted_consent = new ConsentResponse { Error = AuthorizationError.AccessDenied };

                // emit event
                await _Events.RaiseAsync(new ConsentDeniedEvent(User.GetSubjectId(), request.Client.ClientId, request.ValidatedResources.RawScopeValues));
            }
            // user clicked 'yes' - validate the data
            else if (model.Button == "yes")
            {
                // if the user consented to some scope, build the response model
                if (model.ScopesConsented != null && model.ScopesConsented.Any())
                {
                    var scopes = model.ScopesConsented;
                    if (!ConsentOptions.EnableOfflineAccess)
                    {
                        scopes = scopes.Where(x => x != IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess);
                    }

                    granted_consent = new ConsentResponse
                    {
                        RememberConsent = model.RememberConsent,
                        ScopesValuesConsented = scopes.ToArray()
                    };

                    // emit event
                    await _Events.RaiseAsync(new ConsentGrantedEvent(User.GetSubjectId(), request.Client.ClientId, request.ValidatedResources.RawScopeValues, granted_consent.ScopesValuesConsented, granted_consent.RememberConsent));
                }
                else
                {
                    result.ValidationError = ConsentOptions.MustChooseOneErrorMessage;
                }
            }
            else
            {
                result.ValidationError = ConsentOptions.InvalidSelectionErrorMessage;
            }

            if (granted_consent != null)
            {
                // communicate outcome of consent back to identityserver
                await _Interaction.HandleRequestAsync(model.UserCode, granted_consent);

                // indicate that's it ok to redirect back to authorization endpoint
                result.RedirectUri = model.ReturnUrl;
                result.ClientId = request.Client.ClientId;
            }
            else
            {
                // we need to redisplay the consent UI
                result.ViewModel = await BuildViewModelAsync(model.UserCode, model);
            }

            return result;
        }

        private async Task<DeviceAuthorizationViewModel> BuildViewModelAsync(string UserCode, DeviceAuthorizationInputModel model = null)
        {
            var request = await _Interaction.GetAuthorizationContextAsync(UserCode);
            if (request != null)
            {
                return CreateConsentViewModel(UserCode, model, request);
            }

            return null;
        }

        private DeviceAuthorizationViewModel CreateConsentViewModel(string UserCode, DeviceAuthorizationInputModel model, DeviceFlowAuthorizationRequest request)
        {
            var vm = new DeviceAuthorizationViewModel
            {
                UserCode = UserCode,

                RememberConsent = model?.RememberConsent ?? true,
                ScopesConsented = model?.ScopesConsented ?? Enumerable.Empty<string>(),

                ClientName = request.Client.ClientName ?? request.Client.ClientId,
                ClientUrl = request.Client.ClientUri,
                ClientLogoUrl = request.Client.LogoUri,
                AllowRememberConsent = request.Client.AllowRememberConsent
            };

            vm.IdentityScopes = request.ValidatedResources.Resources.IdentityResources.Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null)).ToArray();

            var api_scopes = new List<ScopeViewModel>();
            foreach (var parsed_scope in request.ValidatedResources.ParsedScopes)
            {
                var api_scope = request.ValidatedResources.Resources.FindApiScope(parsed_scope.ParsedName);
                if (api_scope == null) continue;
                var scope_vm = CreateScopeViewModel(parsed_scope, api_scope, vm.ScopesConsented.Contains(parsed_scope.RawValue) || model == null);
                api_scopes.Add(scope_vm);
            }
            if (ConsentOptions.EnableOfflineAccess && request.ValidatedResources.Resources.OfflineAccess)
            {
                api_scopes.Add(GetOfflineAccessScope(vm.ScopesConsented.Contains(IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess) || model == null));
            }
            vm.ResourceScopes = api_scopes;

            return vm;
        }

        private static ScopeViewModel CreateScopeViewModel(IdentityResource identity, bool check) =>
            new ScopeViewModel
            {
                Name = identity.Name,
                DisplayName = identity.DisplayName ?? identity.Name,
                Description = identity.Description,
                Emphasize = identity.Emphasize,
                Required = identity.Required,
                Checked = check || identity.Required
            };

        public ScopeViewModel CreateScopeViewModel(ParsedScopeValue ParsedScopeValue, ApiScope APIScope, bool check) =>
            new ScopeViewModel
            {
                Name = ParsedScopeValue.RawValue,
                // todo: use the parsed scope value in the display?
                DisplayName = APIScope.DisplayName ?? APIScope.Name,
                Description = APIScope.Description,
                Emphasize = APIScope.Emphasize,
                Required = APIScope.Required,
                Checked = check || APIScope.Required
            };

        private static ScopeViewModel GetOfflineAccessScope(bool check) =>
            new ScopeViewModel
            {
                Name = IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess,
                DisplayName = ConsentOptions.OfflineAccessDisplayName,
                Description = ConsentOptions.OfflineAccessDescription,
                Emphasize = true,
                Checked = check
            };
    }
}