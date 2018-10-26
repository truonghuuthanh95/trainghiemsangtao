using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO.HCM_EDU_DATA;

namespace TraiNghiemSangTao.Repositories.Interfaces
{
    public interface IHCMLopRepository
    {
        List<T_DM_Lop> GetT_DM_LopsBySchoolId(string schoolId);
    }
}