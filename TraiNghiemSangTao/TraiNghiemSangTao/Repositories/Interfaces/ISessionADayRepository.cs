﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;

namespace TraiNghiemSangTao.Repositories.Interfaces
{
    public interface ISessionADayRepository
    {
        List<SessionADay> GetSessionADays();
    }
}