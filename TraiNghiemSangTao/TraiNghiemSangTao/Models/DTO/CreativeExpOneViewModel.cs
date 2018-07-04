using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;

namespace TraiNghiemSangTao.Models.DTO
{
    public class CreativeExpOneViewModel
    {
        public RegistrationCreativeExp RegistrationCreativeExp { get; set; }
        public List<Program> Programs { get; set; }
        public List<SchoolDegree> SchoolDegrees { get; set; }
        public List<District> District { get; set; }
        public List<School> Schools { get; set; }
        public List<Class> Classes { get; set; }
        public List<Jobtitle> Jobtitles { get; set; }

        public List<SessionADay> SessionADays { get; set; }

        public CreativeExpOneViewModel(RegistrationCreativeExp registrationCreativeExp, List<Program> programs, List<SchoolDegree> schoolDegrees, List<District> district, List<School> schools, List<Class> classes, List<Jobtitle> jobtitles, List<SessionADay> sessionADays)
        {
            RegistrationCreativeExp = registrationCreativeExp;
            Programs = programs;
            SchoolDegrees = schoolDegrees;
            District = district;
            Schools = schools;
            Classes = classes;
            Jobtitles = jobtitles;
            SessionADays = sessionADays;
        }
    }
}