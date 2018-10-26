using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO.HCM_EDU_DATA;

namespace TraiNghiemSangTao.Repositories.Interfaces
{
    public interface IHCMSchoolRepository
    {
        List<T_DM_Truong> GetT_DM_TruongsByPGDIdId(int pgdId);
    }
}