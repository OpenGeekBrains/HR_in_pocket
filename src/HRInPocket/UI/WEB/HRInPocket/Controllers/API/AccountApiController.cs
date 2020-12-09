using System;
using System.Linq;

using HRInPocket.Domain.Exceptions;
using HRInPocket.Domain.Models.MailSender;
using HRInPocket.Infrastructure.Models;
using HRInPocket.Infrastructure.Models.JsonReturnModels;
using HRInPocket.Infrastructure.Models.Records;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Services;
using HRInPocket.Services.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Controllers.API
{
    [Route(WebAPI.Accounts)]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly IMailSenderService _mailSender;
        private readonly ILogger<AccountApiController> _logger;
        private readonly AuthService _authService;
        
        public AccountApiController(IMailSenderService mailSender, ILogger<AccountApiController> logger, AuthService authService)
        {
            _mailSender = mailSender;
            _logger = logger;
            _authService = authService;
        }

        #region Get
        
        [HttpGet("accounts")]
        public JsonResult GetAccounts()
        {
            // todo: trace about who get data
            _logger.LogInformation(LogEvents.ListItems, "Get accounts data");
            return new JsonResult(new ArrayContent(_authService.GetAccounts(), _authService.GetAccounts().Any()));
        }

        #endregion

        [HttpPost("registration")]
        public IActionResult Registration([FromBody] UserData userData)
        {
            const int logId = LogEvents.AccountRegistration;
            try
            {
                _logger.LogInformation(logId, $"Registration of user {userData.email}");
                var token = _authService.Register(userData);
                // todo: on registered user must be logged in?

                //_mailSender.SendEmailConfirmation(new Mail
                //{
                //    ActionUrl = $@"https://hr-in-your-pocket.ru/auth/?key={AuthService.PublicKey}&token={token}&email={userData.email}",
                //    Body = "Confirm email with link",
                //    RecipientEmail = userData.email,
                //    Subject = "Confirmation email",
                //    UnsubscribeUrl = this.Url.Action("UnsubscribeMailSend", "NotifyApi", userData.email)
                //});
                _logger.LogInformation(LogEvents.MailSending, $"Sended confirmation of email address mail to user {userData.email} on address {userData.email}");
                
                _logger.LogInformation(logId, $"Registration of user {userData.email} completed");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogWarning(LogEvents.AccountRegistrationFailure, e, e.Message);
                return new JsonResult(new Error(e.Message,false,"email"));
            }
        }

        [HttpPost("auth")]
        public IActionResult Login([FromBody] UserData data)
        {
            const int logId = LogEvents.AccountLogin;
            try
            {
                _logger.LogInformation(logId,$"User {data.email} trying to logged in");
                var token = _authService.Login(data);
                
                _logger.LogInformation(logId,$"User {data.email} logged in");
                return Content(token);
            }
            catch (LoginException e)
            {
                _logger.LogWarning(LogEvents.AccountLoginFailure, e, e.Message);
                return new JsonResult(new Error(e.Message, false, e.BadParameter));
            }
        }

        [HttpGet("auth/logout")]
        public IActionResult Logout([FromBody] UserData data)
        {
            try
            {
                _logger.LogInformation(LogEvents.AccountLogout,$"User {data.email} trying to logged out");
                _authService.Logout(data);
                    
                _logger.LogInformation(LogEvents.AccountLogout,$"User {data.email} logged out");
                return Ok();
            }
            catch (LoginException e)
            {
                _logger.LogWarning(LogEvents.AccountLogoutFailure, e, e.Message);
                return new JsonResult(new Error(e.Message, false, e.BadParameter));
            }
        }
    }
}
