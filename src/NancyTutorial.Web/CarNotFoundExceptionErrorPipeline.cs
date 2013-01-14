using System.Text;
using Nancy;
using Nancy.Bootstrapper;

namespace NancyTutorial
{
    public class CarNotFoundExceptionErrorPipeline : IApplicationStartup
    {
        public void Initialize(IPipelines pipelines)
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