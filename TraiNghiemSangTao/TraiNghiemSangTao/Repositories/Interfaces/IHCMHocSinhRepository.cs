using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO.HCM_EDU_DATA;

namespace TraiNghiemSangTao.Repositories.Interfaces
{
    public interface IHCMHocSinhRepository
    {
        List<T_DM_HocSinh> GetT_DM_HocSinhsByClassId(string classId);
    }
}