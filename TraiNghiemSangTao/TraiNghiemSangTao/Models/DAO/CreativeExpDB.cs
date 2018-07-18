namespace TraiNghiemSangTao.Models.DAO
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CreativeExpDB : DbContext
    {
        public CreativeExpDB()
            : base("name=CreativeExpDB13")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Jobtitle> Jobtitles { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<RegistrationCreativeExp> RegistrationCreativeExps { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<SchoolDegree> SchoolDegrees { get; set; }
        public virtual DbSet<SessionADay> SessionADays { get; set; }
        public virtual DbSet<SocialLifeSkill> SocialLifeSkills { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectsRegisted> SubjectsRegisteds { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>()
                .HasMany(e => e.Wards)
                .WithRequired(e => e.District)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Jobtitle>()
                .HasMany(e => e.RegistrationCreativeExps)
                .WithOptional(e => e.Jobtitle)
                .HasForeignKey(e => e.JobTiteId);

            modelBuilder.Entity<Province>()
                .HasMany(e => e.Districts)
                .WithRequired(e => e.Province)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Registration>()
                .Property(e => e.ViTriKienThuc)
                .IsUnicode(false);

            modelBuilder.Entity<Registration>()
                .Property(e => e.TomTatNoiDungCT)
                .IsUnicode(false);

            modelBuilder.Entity<SessionADay>()
                .HasMany(e => e.RegistrationCreativeExps)
                .WithOptional(e => e.SessionADay)
                .HasForeignKey(e => e.DaySessionId);
        }
    }
}
