using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO.HCM_EDU_DATA;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class HCMLopRepository : IHCMLopRepository
    {
        HCM_EDU_DATA _db;

        public HCMLopRepository(HCM_EDU_DATA db)
        {
            _db = db;
        }

        public List<T_DM_Lop> GetT_DM_LopsBySchoolId(string schoolId)
        {
            List<T_DM_Lop> t_DM_Lops = _db.T_DM_Lop.Where(s => s.SchoolID == schoolId).ToList();
            return t_DM_Lops;
        }
    }
}