using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRInPocket.IdentityServer.Controllers.Helpers;
using HRInPocket.IdentityServer.Extensions;
using HRInPocket.IdentityServer.InMemoryConfig;
using HRInPocket.IdentityServer.Models;
using HRInPocket.IdentityServer.ViewModels;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRInPocket.IdentityServer.Controllers
{
    /// <summary>
    /// This controller processes the consent UI
    /// </summary>
    [SecurityHeaders]
    [Authorize]
    public class ConsentController : Controller
    {
        private readonly IIdentityServerInteractionService _Interaction;
        private readonly IClientStore _ClientStore;
        private readonly IResourceStore _ResourceStore;
        private readonly IEventService _Events;
        private readonly ILogger<ConsentController> _Logger;

        public ConsentController(
            IIdentityServerInteractionService Interaction,
            IClientStore ClientStore,
            IResourceStore ResourceStore,
            IEventService Events,
            ILogger<ConsentController> Logger)
        {
            _Interaction = Interaction;
            _ClientStore = ClientStore;
            _ResourceStore = ResourceStore;
            _Events = Events;
            _Logger = Logger;
        }

        /// <summary>
        /// Shows the consent screen
        /// </summary>
        /// <param name="ReturnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(string ReturnUrl)
        {
            var vm = await BuildViewModelAsync(ReturnUrl);
            if (vm != null)
            {
                return View("Index", vm);
            }

            return View("Error");
        }

        /// <summary>
        /// Handles the consent screen postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ConsentInputModel model)
        {
            var result = await ProcessConsent(model);

            if (result.IsRedirect)
            {
                if (await _ClientStore.IsPkceClientAsync(result.ClientId))
                {
                    // if the client is PKCE then we assume it's native, so this change in how to
                    // return the response is for better UX for the end user.
                    return View("Redirect", new RedirectViewModel { RedirectUrl = result.RedirectUri });
                }

                return Redirect(result.RedirectUri);
            }

            if (result.HasValidationError)
            {
                ModelState.AddModelError(string.Empty, result.ValidationError);
            }

            if (result.ShowView)
            {
                return View("Index", result.ViewModel);
            }

            return View("Error");
        }

        /*****************************************/
        /* helper APIs for the ConsentController */
        /*****************************************/
        private async Task<ProcessConsentResult> ProcessConsent(ConsentInputModel model)
        {
            var result = new ProcessConsentResult();

            // validate return url is still valid
            var request = await _Interaction.GetAuthorizationContextAsync(model.ReturnUrl);
            if (request == null) return result;

            var granted_consent = new ConsentResponse();

            // user clicked 'no' - send back the standard 'access_denied' response
            if (model.Button == "no")
            {
                granted_consent.Error = AuthorizationError.AccessDenied;

                // emit event
                await _Events.RaiseAsync(new ConsentDeniedEvent(User.GetSubjectId(), request.Client.ClientId, request.PromptModes));
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
                    await _Events.RaiseAsync(new ConsentGrantedEvent(User.GetSubjectId(), request.Client.ClientId, request.PromptModes, granted_consent.ScopesValuesConsented, granted_consent.RememberConsent));
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
                await _Interaction.GrantConsentAsync(request, granted_consent);

                // indicate that's it ok to redirect back to authorization endpoint
                result.RedirectUri = model.ReturnUrl;
                result.ClientId = request.Client.ClientId;
            }
            else
                // we need to redisplay the consent UI
                result.ViewModel = await BuildViewModelAsync(model.ReturnUrl, model);

            return result;
        }

        private async Task<ConsentViewModel> BuildViewModelAsync(string ReturnUrl, ConsentInputModel model = null)
        {
            var request = await _Interaction.GetAuthorizationContextAsync(ReturnUrl);
            if (request != null)
            {
                var client = await _ClientStore.FindEnabledClientByIdAsync(request.Client.ClientId);
                if (client != null)
                {
                    var resources = await _ResourceStore.FindEnabledResourcesByScopeAsync(request.PromptModes);
                    if (resources != null && (resources.IdentityResources.Any() || resources.ApiResources.Any()))
                    {
                        return CreateConsentViewModel(model, ReturnUrl, request, client, resources);
                    }
                    else
                    {
                        _Logger.LogError("No scopes matching: {0}", request.PromptModes.Aggregate((x, y) => x + ", " + y));
                    }
                }
                else
                {
                    _Logger.LogError("Invalid client id: {0}", request.Client.ClientId);
                }
            }
            else
            {
                _Logger.LogError("No consent request matching request: {0}", ReturnUrl);
            }

            return null;
        }

        private ConsentViewModel CreateConsentViewModel(
            ConsentInputModel model, string ReturnUrl,
            AuthorizationRequest request,
            Client client, Resources resources)
        {
            var vm = new ConsentViewModel
            {
                RememberConsent = model?.RememberConsent ?? true,
                ScopesConsented = model?.ScopesConsented ?? Enumerable.Empty<string>(),

                ReturnUrl = ReturnUrl,

                ClientName = client.ClientName ?? client.ClientId,
                ClientUrl = client.ClientUri,
                ClientLogoUrl = client.LogoUri,
                AllowRememberConsent = client.AllowRememberConsent
            };

            vm.IdentityScopes = resources.IdentityResources.Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null)).ToArray();
            var api_scopes = new List<ScopeViewModel>();
            foreach (var parsed_scope in request.ValidatedResources.ParsedScopes)
            {
                var api_scope = request.ValidatedResources.Resources.FindApiScope(parsed_scope.ParsedName);
                if (api_scope != null)
                {
                    var scope_vm = CreateScopeViewModel(parsed_scope, api_scope, vm.ScopesConsented.Contains(parsed_scope.RawValue) || model == null);
                    api_scopes.Add(scope_vm);
                }
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
                DisplayName = identity.DisplayName,
                Description = identity.Description,
                Emphasize = identity.Emphasize,
                Required = identity.Required,
                Checked = check || identity.Required
            };

        public ScopeViewModel CreateScopeViewModel(ParsedScopeValue ParsedScopeValue, ApiScope APIScope, bool check)
        {
            var display_name = APIScope.DisplayName ?? APIScope.Name;
            if (!String.IsNullOrWhiteSpace(ParsedScopeValue.ParsedParameter))
            {
                display_name += ":" + ParsedScopeValue.ParsedParameter;
            }

            return new ScopeViewModel
            {
                Name = ParsedScopeValue.RawValue,
                DisplayName = display_name,
                Description = APIScope.Description,
                Emphasize = APIScope.Emphasize,
                Required = APIScope.Required,
                Checked = check || APIScope.Required
            };
        }

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