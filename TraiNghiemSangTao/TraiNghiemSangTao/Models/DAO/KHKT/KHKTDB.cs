namespace TraiNghiemSangTao.Models.DAO.KHKT
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class KHKTDB : DbContext
    {
        public KHKTDB()
            : base("name=CreativeExpDB14")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<KHKTLinhVucThamGia> KHKTLinhVucThamGias { get; set; }
        public virtual DbSet<KhoaHocKiThuat> KhoaHocKiThuats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
