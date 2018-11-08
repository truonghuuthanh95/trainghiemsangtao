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

        public KHKTLinhVucThamGia GetHKTLinhVucThamGiaById(int id)
        {
            KHKTLinhVucThamGia kHKTLinhVucThamGia = _db.KHKTLinhVucThamGias.Where(s => s.Id == id).SingleOrDefault();
            return kHKTLinhVucThamGia;
        }

        public List<KHKTLinhVucThamGia> GetKHKTLinhVucThamGias()
        {
            List<KHKTLinhVucThamGia> kHKTLinhVucThamGias = _db.KHKTLinhVucThamGias.Where(s => s.IsActive == true).ToList();
            return kHKTLinhVucThamGias;
           
        }
    }
}