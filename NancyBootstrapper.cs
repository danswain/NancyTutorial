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

        protected override void ApplicationStartup(StructureMap.IContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            pipelines.OnError += (context, exception) =>
            {
                if (exception is CarNotFoundException)
                    return new Response
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        ContentType = "text/html",
                        Contents = (stream) =>
                        {
                            var errorMessage =
                                Encoding.UTF8.GetBytes(
                                    exception.Message);
                            stream.Write(errorMessage, 0,
                                         errorMessage.Length);
                        }
                    };

                return HttpStatusCode.InternalServerError;
            };
        }
    }
}