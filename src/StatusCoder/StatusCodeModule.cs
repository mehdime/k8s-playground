using Nancy;

namespace StatusCoder
{
    public class StatusCodeModule : NancyModule
    {
        public StatusCodeModule()
        {
            // Echoes back the provided status code
            Get("/{statusCode:int}", parameters =>
            {
                return Response.AsJson(
                    model: new
                    {
                        type = "status-echo",
                        title = "Status Code Echo",
                        details = "Just echoing the provided status code back.",
                        status = parameters.statusCode
                    },
                    statusCode: (HttpStatusCode) parameters.statusCode);
            });
        }
    }
}
