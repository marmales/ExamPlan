using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using System.Linq;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using Domain.Concrete;
namespace UniversitySite.Infrastructure
{
    public class NinjectDependecyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependecyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        private void AddBindings()
        {
            kernel.Bind<IStudentsRepository>().To<EFStudentRepository>();
            kernel.Bind<IClassRepository>().To<EFDbClassRepository>();
            kernel.Bind<IExamRepository>().To<EFExamRepository>();
            kernel.Bind<IGradeRepository>().To<EFGradeRepository>();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}