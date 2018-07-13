using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;

namespace TraiNghiemSangTao.Models.DTO
{
    public class NoiDungKhacJson
    {
        public Registration Registration { get; set; }
        public string SubjectRegisted { get; set; }

        public NoiDungKhacJson(Registration registration, string subjectRegisted)
        {
            Registration = registration;
            SubjectRegisted = subjectRegisted;
        }
    }
}