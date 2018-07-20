using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraiNghiemSangTao.Repositories.Implements;
using TraiNghiemSangTao.Repositories.Interfaces;
using Unity;
using Unity.Lifetime;

namespace TraiNghiemSangTao.App_Start
{
    public static class IocConfigration
    {
        public static void ConfigrationIocContainer()
        {
            IUnityContainer container = new UnityContainer();
            registerService(container);
            DependencyResolver.SetResolver(new UnityResolvers(container));

        }

        private static void registerService(IUnityContainer container)
        {
            
            container.RegisterType<ISchoolRepository, SchoolRepository>();
            container.RegisterType<IRegistrationCreativeExpRepository, RegistrationCreativeExpRepository>();
            container.RegisterType<IProvinceRepository, ProvinceRepository>();            
            container.RegisterType<IJobTitleRepository, JobTitleRepository>();
            container.RegisterType<IDistrictRepository, DistrictRepository>();
            container.RegisterType<IClassesRepository, ClassesRepository>();
            container.RegisterType<ISessionADayRepository, SessionADayRepository>();
            container.RegisterType<ISchoolDegreeRepository, SchoolDegreeRepository>();           
            container.RegisterType<IProgramRepository, ProgramRepository>();
            container.RegisterType<ISubjectRepository, SubjectRepository>();
            container.RegisterType<IRegistrationRepository, RegistrationRepository>();
            container.RegisterType<ISubjectRegistedRepository, SubjectRegistedRepository>();
            container.RegisterType<ISocialLifeSkillRepository, SocialLifeSkillRepository>();
            container.RegisterType<IAccountRepository, AccountRepository>();
        }
    }
}