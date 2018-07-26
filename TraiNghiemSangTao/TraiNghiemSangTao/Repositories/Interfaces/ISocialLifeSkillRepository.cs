using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Models.DTO;

namespace TraiNghiemSangTao.Repositories.Interfaces
{
    public interface ISocialLifeSkillRepository
    {
        List<SocialLifeSkill> GetSocialLifeSkillsByDate(DateTime dateFrom, DateTime dateTo);
        SocialLifeSkill UpdateSocialLifeSkill(SocialLifeSkillDTO socialLifeSkillDTO, int id);
        SocialLifeSkill GetSocialLifeSkillById(int id);
        SocialLifeSkill CreateSocialLifeSkillWithFile(string fileKeHoach);
        bool CheckExistedFileKeHoach(string fileKeHoach);
        SocialLifeSkill UpdateSocialLifeSkillWithoutCode(SocialLifeSkillDTO socialLifeSkillDTO, int id);
        bool CheckValidCodeRegisted(string codeRegisted);
        SocialLifeSkill GetSocialLifeSkillByRegistedCode(string registedCode);
        SocialLifeSkill UpdateSocialLifeSkillFileKeHoachById(int id, string fileKeHoachName);
    }
}