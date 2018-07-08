using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;

namespace TraiNghiemSangTao.Models.DTO
{
    public class ListRegistrationCreativeExpOneViewModel
    {
        public List<RegistrationCreativeExp> RegistrationCreativeExps { get; set; }
        public List<Program> Programs { get; set; }
        public ListRegistrationCreativeExpOneViewModel(List<RegistrationCreativeExp> registrationCreativeExps, List<Program> programs)
        {
            RegistrationCreativeExps = registrationCreativeExps;
            Programs = programs;
        }
    }
}