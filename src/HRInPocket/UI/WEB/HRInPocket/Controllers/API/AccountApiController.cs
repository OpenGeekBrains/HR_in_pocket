using System;
using System.Collections.Generic;
using System.Linq;

using HRInPocket.Domain.Models.MailSender;
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

    #region Work Models
    
    public record Account(UserData Data)
    {
        public Guid Token { get; } = Guid.NewGuid();

        public bool IsLoggedIn { get; set; }
    }
    public record UserData(string email, string password);

    public record NotifyUser(string email)
    {
        public bool EmailNotify { get; set; }
        public bool SmsNotify { get; set; }
        public bool PushNotify { get; set; }
    }

    #endregion

    #region Services
    
    public class AuthService
    {
        private static readonly List<Account> Accounts = new List<Account>();
        public const string publicKey = "kfjie33Ff*7";
        private static readonly List<NotifyUser> Notify = new List<NotifyUser>();

        public IEnumerable<Account> GetAccounts()
        {
            for (var i = 0; i < Accounts.Count; i++)
                yield return Accounts[i];
        }

        public IEnumerable<NotifyUser> GetNotifyUsers()
        {
            for (var i = 0; i < Notify.Count; i++)
                yield return Notify[i];
        }

        public string Register(UserData user)
        {
            if (Accounts.FirstOrDefault(a => a.Data.email == user.email) is not null) throw new RegistrationException(user);
            var account = new Account(user);
            Accounts.Add(account);
            return account.Token.ToString();
        }

        public string Login(UserData data)
        {
            var user = CheckUser(data);
            user.IsLoggedIn = true;
            // todo: remember me
            return user.Token.ToString();
        }

        public bool Logout(UserData data)
        {
            var user = CheckUser(data);
            user.IsLoggedIn = false;
            return true;
        }

        public void NotifyMe(NotifyUser notify)
        {
            var user = Notify.Find(n => n.email == notify.email);
            if (user is null) Notify.Add(notify);
            var index = Notify.IndexOf(user);
            Notify[index] = notify;
        }

        public bool UnsubscribeEmail(string email)
        {
            var notify = Notify.Find(n => n.email == email);
            if (notify is null) return false;
            notify.EmailNotify = false;
            return true;
        }

        private static Account CheckUser(UserData data)
        {
            var user = Accounts.Find(a => a.Data == data);
            if (user is not null) return user;

            if (Accounts.SingleOrDefault(a => a.Data.email == data.email) is not null)
                throw new LoginException("Wrong password", nameof(data.password));
            throw new LoginException("Account not existed", nameof(data.email));
        }
    } 
    
    #endregion


    #region Custom Exceptions

    public class AuthorizationException : ApplicationException
    {
        public AuthorizationException(string message, Exception innerException = null) : base(message, innerException)
        {

        }
    }

    public class RegistrationException : AuthorizationException
    {
        public readonly UserData User;

        public RegistrationException(UserData user) : base($"User with {user.email} already exist") => User = user;
    }

    public class LoginException : AuthorizationException
    {
        public readonly string BadParameter;

        public LoginException(string message, string badParameter) : base(message)
        {
            BadParameter = badParameter;
        }
    } 
    
    #endregion
}
