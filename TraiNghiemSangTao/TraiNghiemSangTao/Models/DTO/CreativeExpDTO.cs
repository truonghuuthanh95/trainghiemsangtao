using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraiNghiemSangTao.Models.DTO
{
    public class CreativeExpDTO
    {       
        public int SchoolId { get; set; }

        public int StudentQuantity { get; set; }
    
        public string Creator { get; set; }

        public int JobTiteId { get; set; }

        public int ProgramId { get; set; }
       
        public DateTime DateRegisted { get; set; }

        public int SchoolDegreeId { get; set; }

        public int ClassId { get; set; }

        public int DaySessionId { get; set; }
      
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }

        public string ActivitiTitle { get; set; }
    }
}