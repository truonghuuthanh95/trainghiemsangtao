using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Models.DTO;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Controllers
{
    public class AccountController : Controller
    {
        IAccountRepository accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        // GET: Account
        [Route("login", Name = "login")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Route("requestLogin")]
        [HttpPost]
        public ActionResult RequestLogin(string username, string password)
        {
            Account account = accountRepository.CheckUsernamePassword(username.Trim(), password.Trim());
            if (account == null)
            {
                return Json(new ReturnLoginFormat(400, "Sai tên truy cập hoặc mật khẩu", null));
            }
            if (account.IsActive == false)
            {
                return Json(new ReturnLoginFormat(400, "Tài khoản hiện đang bị khóa", null));
            }
            else
            {
                Session.Add(Utils.CommonConstant.USER_SESSION, account);
                return Json(new ReturnLoginFormat(200, "success", account));
            }
        }
        [Route("logout")]
        [HttpGet]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
    }
}