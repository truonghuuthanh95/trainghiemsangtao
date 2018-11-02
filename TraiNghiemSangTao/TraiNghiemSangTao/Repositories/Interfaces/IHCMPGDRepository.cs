using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO.HCM_EDU_DATA;

namespace TraiNghiemSangTao.Repositories.Interfaces
{
    public interface IHCMPGDRepository
    {
        List<T_DM_PGD> GetT_DM_PGDs();
    }
}