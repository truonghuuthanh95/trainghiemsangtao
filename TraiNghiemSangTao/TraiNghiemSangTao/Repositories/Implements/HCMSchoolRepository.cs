using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO.HCM_EDU_DATA;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class HCMSchoolRepository : IHCMSchoolRepository
    {
        HCM_EDU_DATA _db;

        public HCMSchoolRepository(HCM_EDU_DATA db)
        {
            _db = db;
        }

        public List<T_DM_Truong> GetT_DM_TruongsByPGDIdId(int pgdId)
        {
            List<T_DM_Truong> t_DM_Truongs = _db.T_DM_Truong.Where(s => s.PGDID == pgdId).ToList();
            return t_DM_Truongs;
        }
    }
}