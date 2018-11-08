using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO.KHKT;
using TraiNghiemSangTao.Models.DTO.KHKT;

namespace TraiNghiemSangTao.Repositories.Interfaces
{
    public interface IKHKTKhoaHocKiThuatRepository
    {
        KhoaHocKiThuat CreateKhoaHocKiThuat(KhoaHocKithuatDTO khoaHocKithuatDTO);
        List<KhoaHocKiThuatDetailDTO> GetKhoaHocKiThuatsBySchoolId(string schoolId);
        List<KhoaHocKiThuatDetailDTO> GetKhoaHocKiThuats();
        KhoaHocKiThuat UpdateFileTaiLieuKhoaHocKiThuat(int id, string tenFile);
        KhoaHocKiThuat GetKhoaHocKiThuatById(int id);        
        List<KhoaHocKiThuat> GetKhoaHocKiThuatByDeTaiId(int id);

        bool DeleteKHKTById(int id);

    }
}