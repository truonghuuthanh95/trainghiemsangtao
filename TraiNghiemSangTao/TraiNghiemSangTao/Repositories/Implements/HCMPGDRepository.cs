using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO.HCM_EDU_DATA;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class HCMPGDRepository : IHCMPGDRepository
    {
        HCM_EDU_DATA _db;

        public HCMPGDRepository(HCM_EDU_DATA db)
        {
            _db = db;
        }

        public List<T_DM_PGD> GetT_DM_PGDs()
        {
            List<T_DM_PGD> t_DM_PGDs = _db.T_DM_PGD.ToList();
            return t_DM_PGDs;
        }
    }
}