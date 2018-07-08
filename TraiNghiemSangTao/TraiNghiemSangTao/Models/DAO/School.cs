namespace TraiNghiemSangTao.Models.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("School")]
    public partial class School
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public School()
        {
            Registrations = new HashSet<Registration>();
            RegistrationCreativeExps = new HashSet<RegistrationCreativeExp>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int? DistrictId { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        public int? ProvinceId { get; set; }

        [StringLength(50)]
        public string SchoolCode { get; set; }

        public int? SchoolDegreeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Registration> Registrations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistrationCreativeExp> RegistrationCreativeExps { get; set; }

        public virtual SchoolDegree SchoolDegree { get; set; }
    }
}
