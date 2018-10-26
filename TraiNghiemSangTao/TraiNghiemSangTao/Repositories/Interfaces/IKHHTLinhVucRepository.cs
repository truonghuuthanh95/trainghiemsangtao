using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO.KHKT;

namespace TraiNghiemSangTao.Repositories.Interfaces
{
    public interface IKHHTLinhVucRepository
    {
        List<KHKTLinhVucThamGia> GetKHKTLinhVucThamGias();
    }
}