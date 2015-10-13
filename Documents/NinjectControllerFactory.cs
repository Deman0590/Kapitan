using Documents.Repository.Implementations;
using Documents.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Documents
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        //Извлекаем экземпляр контроллера для заданного контекста запроса и типа контроллера
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            ninjectKernel.Bind<IDocumentRepository>().To<EFDocumentRepository>();
            ninjectKernel.Bind<IDocumentTypeRepository>().To<EFDocumentTypeRepository>();
            ninjectKernel.Bind<IFileRepository>().To<EFFileRepository>();
            ninjectKernel.Bind<IOrganizationListRepository>().To<EFOrganizationListRepository>();
            ninjectKernel.Bind<IVehicleTypeRepository>().To<EFVehicleTypeRepository>();
            ninjectKernel.Bind<ICarRepository>().To<EFCarRepository>();
        }
    }
}