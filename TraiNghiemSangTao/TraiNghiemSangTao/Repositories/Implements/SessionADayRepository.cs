using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class SessionADayRepository : ISessionADayRepository
    {
        CreativeExpDB _db;

        public SessionADayRepository(CreativeExpDB db)
        {
            _db = db;
        }

        public List<SessionADay> GetSessionADays()
        {
            List<SessionADay> sessionADays = _db.SessionADays.ToList();
            return sessionADays;
        }
    }
}