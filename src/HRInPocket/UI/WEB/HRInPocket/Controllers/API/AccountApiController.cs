using System;
using System.Linq;

using HRInPocket.Domain.Models.MailSender;
using HRInPocket.Infrastructure.Models.Exceptions;
using HRInPocket.Infrastructure.Models.JsonReturnModels;
using HRInPocket.Infrastructure.Models.Records;
using HRInPocket.Infrastructure.Services;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Services;

using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Controllers.API
{
    [Route(WebAPI.Accounts)]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly IMailSenderService _mailSender;
        private readonly AuthService _authService;
        
        public AccountApiController(IMailSenderService mailSender)
        {
            _mailSender = mailSender;
            _authService = new AuthService();
        }

        #region Get Users
        
        [HttpGet("accounts")]
        public JsonResult GetAccounts() => new(new ArrayContent(_authService.GetAccounts(), _authService.GetAccounts().Any()));

        [HttpGet("notifies")]
        public JsonResult GetNotifies() => new(new ArrayContent(_authService.GetNotifyUsers(), _authService.GetNotifyUsers().Any())); 
        
        #endregion

        [HttpPost("registration")]
        public IActionResult Registration([FromBody] UserData userData)
        {
            try
            {
                var token = _authService.Register(userData);
                // todo: on registered user must be logged in?
                
                //_mailSender.SendEmailConfirmation(new Mail
                //{
                //    ActionUrl = $@"https://hr-in-your-pocket.ru/auth/?key={AuthService.publicKey}&token={token}&email={userData.email}",
                //    Body = "Confirm email with link",
                //    RecipientEmail = userData.email,
                //    Subject = "Confirmation email",
                //    UnsubscribeUrl = this.Url.Action("UnsubscribeMailSend","AccountApi",userData.email)
                //});
                
                return Ok();
            }
            catch (Exception e)
            {
                return new JsonResult(new Error(e.Message,false,"email"));
            }
        }

        [HttpPost("auth")]
        public IActionResult Login([FromBody] UserData data)
        {
            try
            {
                var token = _authService.Login(data);
                return Content(token);
            }
            catch (LoginException e)
            {
                return new JsonResult(new Error(e.Message, false, e.BadParameter));
            }
        }

        [HttpGet("auth/logout")]
        public IActionResult Logout([FromBody] UserData data)
        {
            try
            {
                return _authService.Logout(data) ? Ok() : BadRequest();
            }
            catch (LoginException e)
            {
                return new JsonResult(new Error(e.Message, false, e.BadParameter));
            }
        }

        
        
        [HttpPost("/auth")]
        public JsonResult CheckAuthEmail([FromQuery] string key, [FromQuery] string token, [FromQuery] string email)
        {
            _authService.NotifyMe(new NotifyUser(email){EmailNotify = true});
            return new JsonResult(new { key, token, email });
        }

        [HttpGet("/unsubscribe")]
        public IActionResult UnsubscribeMailSend(string email) => _authService.UnsubscribeEmail(email) ? BadRequest() : Ok();
    }
}
