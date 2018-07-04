namespace TraiNghiemSangTao.Models.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Registration")]
    public partial class Registration
    {
        public int Id { get; set; }

        public int? SchoolId { get; set; }

        public int? StudentQuantity { get; set; }

        [StringLength(10)]
        public string Creator { get; set; }

        public int? JobTitleId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateRegisted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? SchoolDegreeId { get; set; }

        public int? ClassId { get; set; }

        public int? SubjectsRegistedID { get; set; }

        [StringLength(200)]
        public string ProgramName { get; set; }

        public int? ProvinceId { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public virtual Class Class { get; set; }

        public virtual Jobtitle Jobtitle { get; set; }

        public virtual Province Province { get; set; }

        public virtual School School { get; set; }

        public virtual SchoolDegree SchoolDegree { get; set; }

        public virtual SubjectsRegisted SubjectsRegisted { get; set; }
    }
}
