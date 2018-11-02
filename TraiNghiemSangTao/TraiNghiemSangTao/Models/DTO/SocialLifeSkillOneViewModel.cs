using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;

namespace TraiNghiemSangTao.Models.DTO
{
    public class SocialLifeSkillOneViewModel
    {
        public SocialLifeSkill SocialLifeSkill { get; set; }
        public List<SchoolDegree> SchoolDegrees { get; set; }
       
        public List<School> Schools { get; set; }
        public List<Class> Classes { get; set; }
        public List<Jobtitle> Jobtitles { get; set; }
       
        public List<District> Districts { get; set; }

        public SocialLifeSkillOneViewModel(SocialLifeSkill socialLifeSkill, List<SchoolDegree> schoolDegrees, List<School> schools, List<Class> classes, List<Jobtitle> jobtitles, List<District> districts)
        {
            SocialLifeSkill = socialLifeSkill;
            SchoolDegrees = schoolDegrees;
            Schools = schools;
            Classes = classes;
            Jobtitles = jobtitles;
            Districts = districts;
        }
    }
}