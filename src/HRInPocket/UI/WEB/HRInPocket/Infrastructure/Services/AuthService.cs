using System.Collections.Generic;
using System.Linq;

using HRInPocket.Infrastructure.Models.Exceptions;
using HRInPocket.Infrastructure.Models.Records;

namespace HRInPocket.Infrastructure.Services
{
    public class AuthService
    {
        private static readonly List<Account> Accounts = new();
        public const string PublicKey = "kfjie33Ff*7";
        
        public IEnumerable<Account> GetAccounts()
        {
            for (var i = 0; i < Accounts.Count; i++)
                yield return Accounts[i];
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

        private static Account CheckUser(UserData data)
        {
            var user = Accounts.Find(a => a.Data == data);
            if (user is not null) return user;

            if (Accounts.SingleOrDefault(a => a.Data.email == data.email) is not null)
                throw new LoginException("Wrong password", nameof(data.password));
            throw new LoginException("Account not existed", nameof(data.email));
        }
    }
}