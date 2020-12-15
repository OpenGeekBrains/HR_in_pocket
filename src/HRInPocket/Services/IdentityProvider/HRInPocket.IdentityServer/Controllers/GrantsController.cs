using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRInPocket.IdentityServer.Controllers.Helpers;
using HRInPocket.IdentityServer.ViewModels;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.IdentityServer.Controllers
{
    /// <summary>
    /// This sample controller allows a user to revoke grants given to clients
    /// </summary>
    [SecurityHeaders]
    [Authorize]
    public class GrantsController : Controller
    {
        private readonly IIdentityServerInteractionService _Interaction;
        private readonly IClientStore _Clients;
        private readonly IResourceStore _Resources;
        private readonly IEventService _Events;

        public GrantsController(IIdentityServerInteractionService Interaction,
            IClientStore Clients,
            IResourceStore Resources,
            IEventService Events)
        {
            _Interaction = Interaction;
            _Clients = Clients;
            _Resources = Resources;
            _Events = Events;
        }

        /// <summary>
        /// Show list of grants
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View("Index", await BuildViewModelAsync());
        }

        /// <summary>
        /// Handle postback to revoke a client
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Revoke(string ClientId)
        {
            await _Interaction.RevokeUserConsentAsync(ClientId);
            await _Events.RaiseAsync(new GrantsRevokedEvent(User.GetSubjectId(), ClientId));

            return RedirectToAction("Index");
        }

        private async Task<GrantsViewModel> BuildViewModelAsync()
        {
            var grants = await _Interaction.GetAllUserGrantsAsync();

            var list = new List<GrantViewModel>();
            foreach (var grant in grants)
            {
                var client = await _Clients.FindClientByIdAsync(grant.ClientId);
                if (client != null)
                {
                    var resources = await _Resources.FindResourcesByScopeAsync(grant.Scopes);

                    var item = new GrantViewModel()
                    {
                        ClientId = client.ClientId,
                        ClientName = client.ClientName ?? client.ClientId,
                        ClientLogoUrl = client.LogoUri,
                        ClientUrl = client.ClientUri,
                        Created = grant.CreationTime,
                        Expires = grant.Expiration,
                        IdentityGrantNames = resources.IdentityResources.Select(x => x.DisplayName ?? x.Name).ToArray(),
                        ApiGrantNames = resources.ApiResources.Select(x => x.DisplayName ?? x.Name).ToArray()
                    };

                    list.Add(item);
                }
            }

            return new GrantsViewModel
            {
                Grants = list
            };
        }
    }
}