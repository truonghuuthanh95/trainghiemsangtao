using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;

namespace TraiNghiemSangTao.Models.DTO
{
    public class ManagerSocialLifeSkillOneViewModel
    {
        public List<SocialLifeSkill> SocialLifeSkills { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public ManagerSocialLifeSkillOneViewModel(List<SocialLifeSkill> socialLifeSkills, DateTime dateFrom, DateTime dateTo)
        {
            SocialLifeSkills = socialLifeSkills;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }
    }
}