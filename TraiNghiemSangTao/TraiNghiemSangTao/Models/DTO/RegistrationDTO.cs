using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TraiNghiemSangTao.Models.DTO
{
    public class RegistrationDTO
    {
       

        public int? SchoolId { get; set; }

        public int? StudentQuantity { get; set; }

        [StringLength(10)]
        public string Creator { get; set; }

        public int? JobTitleId { get; set; }
      
        public DateTime? DateRegisted { get; set; }
             
        public int? SchoolDegreeId { get; set; }

        public int? ClassId { get; set; }

        [StringLength(200)]
        public string ProgramName { get; set; }

        public int? ProvinceId { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }     

        //[StringLength(500)]
        //public string PhuongAnChoHsThamGia { get; set; }

        public string ViTriKienThuc { get; set; }
       
        public string TomTatNoiDungCT { get; set; }
        public string SubjectSelected { get; set; }
    }
}