using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Models.DTO;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class SocialLifeSkillRepository : ISocialLifeSkillRepository
    {
        CreativeExpDB _db;

        public SocialLifeSkillRepository(CreativeExpDB db)
        {
            _db = db;
        }
       
        public SocialLifeSkill UpdateSocialLifeSkill(SocialLifeSkillDTO socialLifeSkillDTO, int id)
        {
            SocialLifeSkill socialLifeSkill = GetSocialLifeSkillById(id);
            socialLifeSkill.ClassId = socialLifeSkillDTO.ClassId;
            socialLifeSkill.CompanyContact = socialLifeSkillDTO.CompanyContact;
            socialLifeSkill.CreatedAt = DateTime.Now;
            socialLifeSkill.Creatot = socialLifeSkillDTO.Creatot;
            socialLifeSkill.DateFrom = socialLifeSkillDTO.DateFrom;
            socialLifeSkill.DateTo = socialLifeSkillDTO.DateTo;
            socialLifeSkill.Email = socialLifeSkillDTO.Email;
            socialLifeSkill.IsKyNangThucHanhXH = socialLifeSkillDTO.IsKyNangThucHanhXH;
            socialLifeSkill.JobtitleId = socialLifeSkillDTO.JobtitleId;
            socialLifeSkill.License = socialLifeSkillDTO.License;
            socialLifeSkill.PhoneNumber = socialLifeSkillDTO.PhoneNumber;
            socialLifeSkill.SchoolDegreeId = socialLifeSkillDTO.SchoolDegreeId;
            socialLifeSkill.SchoolId = socialLifeSkillDTO.SchoolId;
            socialLifeSkill.SumaryContent = socialLifeSkillDTO.SumaryContent;
            socialLifeSkill.ProgramName = socialLifeSkillDTO.ProgramName;
            socialLifeSkill.CodeRegisted = Utils.RandomCodeRegisted.GetRandomString();
            var existedCodeRegisted = _db.RegistrationCreativeExps.Where(s => s.CodeRegisted == socialLifeSkill.CodeRegisted);
            if (existedCodeRegisted != null)
            {
                do
                {
                    socialLifeSkill.CodeRegisted = Utils.RandomCodeRegisted.GetRandomString();
                    var existedCodeRegistedSecond = _db.RegistrationCreativeExps.Where(s => s.CodeRegisted == socialLifeSkill.CodeRegisted);
                } while (existedCodeRegisted == null);
            }
            _db.Entry(socialLifeSkill).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
            }
            catch (Exception)
            {

                return socialLifeSkill = null;
            }
            return socialLifeSkill;
        }

        public SocialLifeSkill GetSocialLifeSkillById(int id)
        {
            SocialLifeSkill socialLifeSkill = _db.SocialLifeSkills
                 .Include("School")
                .Include("Class")
                .Include("JobTitle")
                .Where(s => s.Id == id).FirstOrDefault();
            return socialLifeSkill;               
        }

        public List<SocialLifeSkill> GetSocialLifeSkillsByDate(DateTime dateFrom, DateTime dateTo)
        {
            List<SocialLifeSkill> socialLifeSkills = _db.SocialLifeSkills
                .Include("School")
                .Include("Class")
                .Include("JobTitle")
                .Where(s => s.DateFrom >= dateFrom && s.DateFrom <= dateTo).OrderBy(s => s.DateFrom).ToList();
            return socialLifeSkills;
        }

        public SocialLifeSkill CreateSocialLifeSkillWithFile(string fileKeHoach)
        {
            SocialLifeSkill socialLifeSkill = new SocialLifeSkill();
            socialLifeSkill.FileKeHoach = fileKeHoach.Trim();
            _db.SocialLifeSkills.Add(socialLifeSkill);
            _db.SaveChanges();
            return socialLifeSkill;
        }

        public bool CheckExistedFileKeHoach(string fileKeHoach)
        {
            SocialLifeSkill socialLifeSkill = _db.SocialLifeSkills.Where(s => s.FileKeHoach == fileKeHoach).FirstOrDefault();
            if (socialLifeSkill == null)
            {
                return false;
            }
            return true;
        }

        public SocialLifeSkill UpdateSocialLifeSkillWithoutCode(SocialLifeSkillDTO socialLifeSkillDTO, int id)
        {
            SocialLifeSkill socialLifeSkill = GetSocialLifeSkillById(id);
            socialLifeSkill.ClassId = socialLifeSkillDTO.ClassId;
            socialLifeSkill.CompanyContact = socialLifeSkillDTO.CompanyContact;
            socialLifeSkill.CreatedAt = DateTime.Now;
            socialLifeSkill.Creatot = socialLifeSkillDTO.Creatot;
            socialLifeSkill.DateFrom = socialLifeSkillDTO.DateFrom;
            socialLifeSkill.DateTo = socialLifeSkillDTO.DateTo;
            socialLifeSkill.Email = socialLifeSkillDTO.Email;
            socialLifeSkill.IsKyNangThucHanhXH = socialLifeSkillDTO.IsKyNangThucHanhXH;
            socialLifeSkill.JobtitleId = socialLifeSkillDTO.JobtitleId;
            socialLifeSkill.License = socialLifeSkillDTO.License;
            socialLifeSkill.PhoneNumber = socialLifeSkillDTO.PhoneNumber;
            socialLifeSkill.SchoolDegreeId = socialLifeSkillDTO.SchoolDegreeId;
            socialLifeSkill.SchoolId = socialLifeSkillDTO.SchoolId;
            socialLifeSkill.SumaryContent = socialLifeSkillDTO.SumaryContent;
            socialLifeSkill.ProgramName = socialLifeSkillDTO.ProgramName;          
            _db.Entry(socialLifeSkill).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
            }
            catch (Exception)
            {

                return socialLifeSkill = null;
            }
            return socialLifeSkill;
        }

        public bool CheckValidCodeRegisted(string codeRegisted)
        {
            var isValid = _db.SocialLifeSkills.Where(s => s.CodeRegisted == codeRegisted).FirstOrDefault();
            if (isValid != null)
            {
                return true;
            }
            return false;
        }

        public SocialLifeSkill GetSocialLifeSkillByRegistedCode(string registedCode)
        {
            SocialLifeSkill socialLifeSkill = _db.SocialLifeSkills
                .Include("School")
                .Include("JobTitle")
                .Include("Class")
                .Where(s => s.CodeRegisted == registedCode).FirstOrDefault();
            return socialLifeSkill;
        }

        public SocialLifeSkill UpdateSocialLifeSkillFileKeHoachById(int id, string fileKeHoachName)
        {
            SocialLifeSkill socialLifeSkill = _db.SocialLifeSkills.Where(s => s.Id == id).SingleOrDefault();
            socialLifeSkill.FileKeHoach = fileKeHoachName;
            _db.Entry(socialLifeSkill).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
            }
            catch (Exception)
            {

                return socialLifeSkill = null;
            }
            return socialLifeSkill;
        }

        
    }
}