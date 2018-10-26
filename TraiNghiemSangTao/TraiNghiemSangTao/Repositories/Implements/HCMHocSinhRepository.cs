using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO.HCM_EDU_DATA;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class HCMHocSinhRepository : IHCMHocSinhRepository
    {
        HCM_EDU_DATA _db;

        public HCMHocSinhRepository(HCM_EDU_DATA db)
        {
            _db = db;
        }

        
        public List<T_DM_HocSinh> GetT_DM_HocSinhsByClassId(string classId)
        {
            List<T_DM_HocSinh> t_DM_HocSinhs = _db.T_DM_HocSinh.Where(s => s.LopID == classId).ToList();
            return t_DM_HocSinhs;
        }
    }
}