using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO.HCM_EDU_DATA;
using TraiNghiemSangTao.Models.DAO.KHKT;
using TraiNghiemSangTao.Models.DTO.KHKT;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class KHKTKhoaHocKiThuatRepository : IKHKTKhoaHocKiThuatRepository
    {
        KHKTDB _db;
        HCM_EDU_DATA _dbHCM;

        public KHKTKhoaHocKiThuatRepository(KHKTDB db, HCM_EDU_DATA dbHCM)
        {
            _db = db;
            _dbHCM = dbHCM;
        }

        public KhoaHocKiThuat CreateKhoaHocKiThuat(KhoaHocKithuatDTO khoaHocKithuatDTO)
        {
            KhoaHocKiThuat khoaHocKiThuat = new KhoaHocKiThuat();
            khoaHocKiThuat.DongGopHs1 = khoaHocKithuatDTO.DongGopHs1;
            khoaHocKiThuat.DongGopHs2 = khoaHocKithuatDTO.DongGopHs2;
            khoaHocKiThuat.HocSinh1 = khoaHocKithuatDTO.HocSinh1;
            khoaHocKiThuat.HocSinh2 = khoaHocKithuatDTO.HocSinh2;
            khoaHocKiThuat.IsCaNhan = khoaHocKithuatDTO.IsCaNhan;
            khoaHocKiThuat.LinhVucId = khoaHocKithuatDTO.LinhVucId;
            khoaHocKiThuat.LopIdHocSinh1 = khoaHocKithuatDTO.LopIdHocSinh1;
            khoaHocKiThuat.LopIdhocSinh2 = khoaHocKithuatDTO.LopIdhocSinh2;
            khoaHocKiThuat.TenDeTai = khoaHocKithuatDTO.TenDeTai;
            khoaHocKiThuat.SchoolId = khoaHocKithuatDTO.SchoolId;
            khoaHocKiThuat.CreatedAt = DateTime.Now;
            khoaHocKiThuat.GVHD = khoaHocKithuatDTO.GVHD;
            khoaHocKiThuat.Email = khoaHocKithuatDTO.Email;
            khoaHocKiThuat.SDT = khoaHocKithuatDTO.SDT;
            _db.KhoaHocKiThuats.Add(khoaHocKiThuat);
            _db.SaveChanges();
            return khoaHocKiThuat;
        }

        public bool DeleteKHKT(int id)
        {
            KhoaHocKiThuat khoaHocKiThuat = GetKhoaHocKiThuatById(id);
            _db.KhoaHocKiThuats.Remove(khoaHocKiThuat);
            _db.SaveChanges();
            return true;
        }

        public KhoaHocKiThuat GetKhoaHocKiThuatById(int id)
        {
            KhoaHocKiThuat khoaHocKiThuat = _db.KhoaHocKiThuats.Where(s => s.Id == id).SingleOrDefault();
            return khoaHocKiThuat;
        }

        public List<KhoaHocKiThuatDetailDTO> GetKhoaHocKiThuats()
        {
            List<KhoaHocKiThuat> dsDaDangKi = _db.KhoaHocKiThuats.ToList();
            List<KhoaHocKiThuatDetailDTO> khoaHocKiThuatDetailDTOs = new List<KhoaHocKiThuatDetailDTO>();
            if (dsDaDangKi.Count() > 0)
            {
                foreach (var item in dsDaDangKi)
                {
                    KhoaHocKiThuatDetailDTO khoaHocKiThuatDetailDTO = new KhoaHocKiThuatDetailDTO();
                    if (item.HocSinh2 != null)
                    {
                        khoaHocKiThuatDetailDTO.KhoaHocKiThuat = item;
                        khoaHocKiThuatDetailDTO.KHKTLinhVucThamGia = _db.KHKTLinhVucThamGias.Where(s => s.Id == item.LinhVucId).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_HocSinh1 = _dbHCM.T_DM_HocSinh.Where(s => s.HocSinhID == item.HocSinh1).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_HocSinh2 = _dbHCM.T_DM_HocSinh.Where(s => s.HocSinhID == item.HocSinh2).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_Truong1 = _dbHCM.T_DM_Truong.Where(s => s.SchoolID == khoaHocKiThuatDetailDTO.T_DM_HocSinh1.SchoolID).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_Truong2 = _dbHCM.T_DM_Truong.Where(s => s.SchoolID == khoaHocKiThuatDetailDTO.T_DM_HocSinh2.SchoolID).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_Lop1 = _dbHCM.T_DM_Lop.Where(s => s.LopID == khoaHocKiThuatDetailDTO.T_DM_HocSinh1.LopID).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_Lop2 = _dbHCM.T_DM_Lop.Where(s => s.LopID == khoaHocKiThuatDetailDTO.T_DM_HocSinh2.LopID).SingleOrDefault();
                        khoaHocKiThuatDetailDTOs.Add(khoaHocKiThuatDetailDTO);
                    }
                    else
                    {
                        khoaHocKiThuatDetailDTO.KhoaHocKiThuat = item;
                        khoaHocKiThuatDetailDTO.KHKTLinhVucThamGia = _db.KHKTLinhVucThamGias.Where(s => s.Id == item.LinhVucId).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_HocSinh1 = _dbHCM.T_DM_HocSinh.Where(s => s.HocSinhID == item.HocSinh1).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_Truong1 = _dbHCM.T_DM_Truong.Where(s => s.SchoolID == khoaHocKiThuatDetailDTO.T_DM_HocSinh1.SchoolID).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_Lop1 = _dbHCM.T_DM_Lop.Where(s => s.LopID == khoaHocKiThuatDetailDTO.T_DM_HocSinh1.LopID).SingleOrDefault();
                        khoaHocKiThuatDetailDTOs.Add(khoaHocKiThuatDetailDTO);
                    }
                }


            }
            return khoaHocKiThuatDetailDTOs;
        }

        public List<KhoaHocKiThuatDetailDTO> GetKhoaHocKiThuatsBySchoolId(string schoolId)
        {
            List<KhoaHocKiThuat> dsDaDangKi = _db.KhoaHocKiThuats.Where(s => s.SchoolId == schoolId).ToList();
            List<KhoaHocKiThuatDetailDTO> khoaHocKiThuatDetailDTOs = new List<KhoaHocKiThuatDetailDTO>();
            if (dsDaDangKi.Count() > 0)
            {                
                foreach (var item in dsDaDangKi)
                {
                    KhoaHocKiThuatDetailDTO khoaHocKiThuatDetailDTO = new KhoaHocKiThuatDetailDTO();
                    if (item.HocSinh2 != null)
                    {                       
                        khoaHocKiThuatDetailDTO.KhoaHocKiThuat = item;
                        khoaHocKiThuatDetailDTO.KHKTLinhVucThamGia = _db.KHKTLinhVucThamGias.Where(s => s.Id == item.LinhVucId).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_HocSinh1 = _dbHCM.T_DM_HocSinh.Where(s => s.HocSinhID == item.HocSinh1).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_HocSinh2 = _dbHCM.T_DM_HocSinh.Where(s => s.HocSinhID == item.HocSinh2).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_Truong1 = _dbHCM.T_DM_Truong.Where(s => s.SchoolID == khoaHocKiThuatDetailDTO.T_DM_HocSinh1.SchoolID).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_Truong2 = _dbHCM.T_DM_Truong.Where(s => s.SchoolID == khoaHocKiThuatDetailDTO.T_DM_HocSinh2.SchoolID).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_Lop1 = _dbHCM.T_DM_Lop.Where(s => s.LopID == khoaHocKiThuatDetailDTO.T_DM_HocSinh1.LopID).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_Lop2 = _dbHCM.T_DM_Lop.Where(s => s.LopID == khoaHocKiThuatDetailDTO.T_DM_HocSinh2.LopID).SingleOrDefault();
                        khoaHocKiThuatDetailDTOs.Add(khoaHocKiThuatDetailDTO);
                    }
                    else
                    {                        
                        khoaHocKiThuatDetailDTO.KhoaHocKiThuat = item;
                        khoaHocKiThuatDetailDTO.KHKTLinhVucThamGia = _db.KHKTLinhVucThamGias.Where(s => s.Id == item.LinhVucId).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_HocSinh1 = _dbHCM.T_DM_HocSinh.Where(s => s.HocSinhID == item.HocSinh1).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_Truong1 = _dbHCM.T_DM_Truong.Where(s => s.SchoolID == khoaHocKiThuatDetailDTO.T_DM_HocSinh1.SchoolID).SingleOrDefault();
                        khoaHocKiThuatDetailDTO.T_DM_Lop1 = _dbHCM.T_DM_Lop.Where(s => s.LopID == khoaHocKiThuatDetailDTO.T_DM_HocSinh1.LopID).SingleOrDefault();
                        khoaHocKiThuatDetailDTOs.Add(khoaHocKiThuatDetailDTO);
                    }
                }
                                
               
            }
            return khoaHocKiThuatDetailDTOs;
            
        }

        public KhoaHocKiThuat UpdateFileTaiLieuKhoaHocKiThuat(int id, string tenFile)
        {
            KhoaHocKiThuat khoaHocKiThuat = _db.KhoaHocKiThuats.Where(s => s.Id == id).SingleOrDefault();
            khoaHocKiThuat.FileTaiLieu = tenFile.Trim();
            _db.Entry(khoaHocKiThuat).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return null;

            }
            return khoaHocKiThuat;
        }
    }
}