using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class AccountRepository : IAccountRepository
    {
        CreativeExpDB _db;

        public AccountRepository(CreativeExpDB db)
        {
            _db = db;
        }

        public Account CheckUsernamePassword(string username, string password)
        {
            Account account = _db.Accounts.Where(s => s.Username == username && s.Password == password).FirstOrDefault();
            if (account != null)
            {
                account.Username = "";
                account.Password = "";
                return account;
            }
            return account;

        }

        
    }
}