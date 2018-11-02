using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO.KHKT;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class KHHTLinhVucRepository : IKHHTLinhVucRepository
    {
        KHKTDB _db;

        public KHHTLinhVucRepository(KHKTDB db)
        {
            _db = db;
        }

        public List<KHKTLinhVucThamGia> GetKHKTLinhVucThamGias()
        {
            List<KHKTLinhVucThamGia> kHKTLinhVucThamGias = _db.KHKTLinhVucThamGias.Where(s => s.IsActive == true).ToList();
            return kHKTLinhVucThamGias;
           
        }
    }
}