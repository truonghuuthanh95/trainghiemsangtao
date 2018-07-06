using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;

namespace TraiNghiemSangTao.Models.DTO
{
    public class NoiDungKhacOneViewModel
    {
        public Registration RegistrationCreativeExp { get; set; }
        public List<SchoolDegree> SchoolDegrees { get; set; }
        public List<Province> Province { get; set; }
        public List<School> Schools { get; set; }
        public List<Class> Classes { get; set; }
        public List<Jobtitle> Jobtitles { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<District> Districts { get; set; }

        public NoiDungKhacOneViewModel(Registration registrationCreativeExp, List<SchoolDegree> schoolDegrees, List<Province> province, List<School> schools, List<Class> classes, List<Jobtitle> jobtitles, List<Subject> subjects, List<District> districts)
        {
            RegistrationCreativeExp = registrationCreativeExp;
            SchoolDegrees = schoolDegrees;
            Province = province;
            Schools = schools;
            Classes = classes;
            Jobtitles = jobtitles;
            Subjects = subjects;
            Districts = districts;
        }
    }
}