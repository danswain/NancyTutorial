using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Nancy;
using Nancy.Bootstrappers.StructureMap;

namespace NancyTutorial
{
    public class NancyBootstrapper : StructureMapNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(StructureMap.IContainer existingContainer)
        {
            StructureMapContainer.Configure(existingContainer);
        }

//        This code has been moved to CarNotFoundExceptionErrorPipeline to make testing easier
//
//        protected override void ApplicationStartup(StructureMap.IContainer container, Nancy.Bootstrapper.IPipelines pipelines)
//        {            
//            base.ApplicationStartup(container, pipelines);
//        }
        

    }
}