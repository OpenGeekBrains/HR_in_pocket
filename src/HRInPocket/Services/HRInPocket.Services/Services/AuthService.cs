using System.Collections.Generic;
using System.Linq;

namespace HRInPocket.Services.Services
{
    public class AuthService
    {
        private static readonly List<Account> Accounts = new List<Account>();
        public const string PublicKey = "kfjie33Ff*7";
        
        public IEnumerable<Account> GetAccounts()
        {
            for (var i = 0; i < Accounts.Count; i++)
                yield return Accounts[i];
        }

        public string Register(UserData user)
        {
            if (Accounts.FirstOrDefault(a => a.Data.email == user.email) != null) throw new RegistrationException(user);
            
            var account = new Account(user);
            Accounts.Add(account);
            return account.Token.ToString();
        }

        public string Login(UserData data)
        {
            var user = CheckUser(data);
            user.IsLoggedIn = true;
            return user.Token.ToString();
        }

        public void Logout(UserData data)
        {
            var user = CheckUser(data);
            user.IsLoggedIn = false;
        }

        private static Account CheckUser(UserData data)
        {
            var user = Accounts.Find(a => a.Data == data);
            if (user != null) return user;

            if (Accounts.SingleOrDefault(a => a.Data.email == data.email) != null)
                throw new LoginException("Wrong password", nameof(data.password));
            throw new LoginException("Account not existed", nameof(data.email));
        }
    }
}