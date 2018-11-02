using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraiNghiemSangTao.Models.DAO.HCM_EDU_DATA;
using TraiNghiemSangTao.Models.DAO.KHKT;
using TraiNghiemSangTao.Models.DTO;
using TraiNghiemSangTao.Models.DTO.KHKT;
using TraiNghiemSangTao.Repositories.Interfaces;
using TraiNghiemSangTao.Utils;

namespace TraiNghiemSangTao.Controllers
{
    [RoutePrefix("khoahockythuat")]
    public class KhoaHocSangTaoController : Controller
    {
        IHCMLopRepository hCMLopRepository;
        IHCMHocSinhRepository hCMHocSinhRepository;
        IKHHTLinhVucRepository kHHTLinhVucRepository;
        IKHKTKhoaHocKiThuatRepository kHKTKhoaHocKiThuatRepository;
        IHCMPGDRepository hCMPGDRepository;
        IHCMSchoolRepository hCMSchoolRepository;

        public KhoaHocSangTaoController(IHCMLopRepository hCMLopRepository, IHCMHocSinhRepository hCMHocSinhRepository, IKHHTLinhVucRepository kHHTLinhVucRepository, IKHKTKhoaHocKiThuatRepository kHKTKhoaHocKiThuatRepository, IHCMPGDRepository hCMPGDRepository, IHCMSchoolRepository hCMSchoolRepository)
        {
            this.hCMLopRepository = hCMLopRepository;
            this.hCMHocSinhRepository = hCMHocSinhRepository;
            this.kHHTLinhVucRepository = kHHTLinhVucRepository;
            this.kHKTKhoaHocKiThuatRepository = kHKTKhoaHocKiThuatRepository;
            this.hCMPGDRepository = hCMPGDRepository;
            this.hCMSchoolRepository = hCMSchoolRepository;
        }

        // GET: KhoaHocSangTao
        [Route("index")]
        public ActionResult Index()
        {
            T_User t_User = (T_User)Session[CommonConstant.KHKT_USER_SESSION];
            if (t_User == null)
            {
                return RedirectToRoute("khoahockithuatdangnhap");
            }
            List<KhoaHocKiThuatDetailDTO> hocKiThuatDetailDTOs = kHKTKhoaHocKiThuatRepository.GetKhoaHocKiThuatsBySchoolId(t_User.DonViID);
            ViewBag.DSKHKT = hocKiThuatDetailDTOs;

            return View();
        }
        [Route("dangnhap", Name = "khoahockithuatdangnhap")]
        public ActionResult DangNhap()
        {

            List<T_DM_PGD> t_DM_PGDs = hCMPGDRepository.GetT_DM_PGDs();
            ViewBag.PGD = t_DM_PGDs;
            List<T_DM_Truong> t_DM_Truongs = hCMSchoolRepository.GetT_DM_TruongsByPGDIdId(t_DM_PGDs[0].PGDID);
            ViewBag.School = t_DM_Truongs;
            return View();
        }
        [Route("requestdangnhap")]
        [HttpPost]
        public ActionResult RequestDangNhap(string password, string schoolId)
        {
            using (HCM_EDU_DATA _db = new HCM_EDU_DATA())
            {
                T_User user = _db.T_User.Where(s => s.DonViID == schoolId).SingleOrDefault();
                if (user == null)
                {
                    return Json(new ReturnFormat(400, "Không tồn tại trường", null), JsonRequestBehavior.AllowGet);
                }
                //var isValid = _db.Database.SqlQuery<string>("SELECT" + user.Password + " = dbo.F_Password(" + user.PasswordSalt+ ", " + password + ") FROM dbo.T_User WHERE UserID =" + user.UserID).FirstOrDefault();
                string isValid = _db.Database.SqlQuery<string>("SELECT dbo.F_Password('" + user.PasswordSalt + "','" + password + "')").FirstOrDefault();
                if (isValid == user.Password)
                {
                    Session.Add(CommonConstant.KHKT_USER_SESSION, user);
                    return Json(new ReturnFormat(200, "success", null), JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new ReturnFormat(400, "Sai mật khẩu", null), JsonRequestBehavior.AllowGet);
        }
        [Route("dangki")]
        public ActionResult Dangki()
        {
            T_User t_User = (T_User)Session[CommonConstant.KHKT_USER_SESSION];
            if (t_User == null)
            {
                return RedirectToRoute("khoahockithuatdangnhap");
            }
            List<T_DM_Lop> t_DM_Lops = hCMLopRepository.GetT_DM_LopsBySchoolId(t_User.DonViID);
            List<T_DM_HocSinh> t_DM_HocSinhs = hCMHocSinhRepository.GetT_DM_HocSinhsByClassId(t_DM_Lops[0].LopID.Trim());
            ViewBag.Lop = t_DM_Lops;
            ViewBag.HocSinh = t_DM_HocSinhs;
            List<KHKTLinhVucThamGia> kHKTLinhVucThamGias = kHHTLinhVucRepository.GetKHKTLinhVucThamGias();
            ViewBag.LinhVuc = kHKTLinhVucThamGias;
            return View();
        }
        [Route("gethocsinhbylopid/{lopId}")]
        [HttpGet]
        public ActionResult GetHocSinh(string lopId)
        {
            T_User t_User = (T_User)Session[CommonConstant.KHKT_USER_SESSION];
            if (t_User == null)
            {
                return Json(new ReturnFormat(403, "Access denied", null), JsonRequestBehavior.AllowGet);
            }
            List<T_DM_HocSinh> t_DM_HocSinhs = hCMHocSinhRepository.GetT_DM_HocSinhsByClassId(lopId.Trim());
            return Json(new ReturnFormat(200, "success", t_DM_HocSinhs), JsonRequestBehavior.AllowGet);
        }
        [Route("submitdangki")]
        [HttpPost]
        public ActionResult DangKiKHKT(KhoaHocKithuatDTO khoaHocKithuatDTO)
        {
            T_User t_User = (T_User)Session[CommonConstant.KHKT_USER_SESSION];
            if (t_User == null)
            {
                return Json(new ReturnFormat(403, "Access denied", null), JsonRequestBehavior.AllowGet);
            }
            khoaHocKithuatDTO.SchoolId = t_User.DonViID;
            KhoaHocKiThuat khoaHocKiThuat = kHKTKhoaHocKiThuatRepository.CreateKhoaHocKiThuat(khoaHocKithuatDTO);
            if (khoaHocKiThuat == null)
            {
                return Json(new ReturnFormat(400, "failed", null), JsonRequestBehavior.AllowGet);
            }
            return Json(new ReturnFormat(200, "success", khoaHocKiThuat.Id), JsonRequestBehavior.AllowGet);
        }
        [Route("getschoolbypgd/{pgd}")]
        [HttpGet]
        public ActionResult GetSchoolByPGD(int pgd)
        {
            List<T_DM_Truong> t_DM_Truongs = hCMSchoolRepository.GetT_DM_TruongsByPGDIdId(pgd);
            return Json(new ReturnFormat(200, "success", t_DM_Truongs), JsonRequestBehavior.AllowGet);
        }
        [Route("uploadfiletailieu")]
        [HttpPost]
        public ActionResult UploadFileTaiLieu(int id, HttpPostedFileBase fileTaiLieu)
        {
            try
            {
                if (fileTaiLieu.ContentLength > 0)
                {
                    //string filename = Path.GetFileName(fileTaiLieu.FileName);
                    KhoaHocKiThuat khoaHocKiThuat = kHKTKhoaHocKiThuatRepository.GetKhoaHocKiThuatById(id);
                    if (khoaHocKiThuat == null)
                    {
                        return Json("failed");
                    }
                    string filename =  khoaHocKiThuat.LinhVucId.ToString() + '-' + khoaHocKiThuat.Id.ToString() + Path.GetExtension(fileTaiLieu.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles/KhoaHocKiThuat"), filename);
                    fileTaiLieu.SaveAs(_path);                    
                    kHKTKhoaHocKiThuatRepository.UpdateFileTaiLieuKhoaHocKiThuat(id, filename.Trim());
                    return Json(new ReturnFormat(200, "success", null), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    kHKTKhoaHocKiThuatRepository.DeleteKHKT(id);
                    return Json("failed");
                }

            }
            catch
            {
                kHKTKhoaHocKiThuatRepository.DeleteKHKT(id);
                return Json("failed");
            }
        }
    }
}