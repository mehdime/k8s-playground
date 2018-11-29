using Nancy;

namespace StatusCoder
{
    public class HealthCheckModule : NancyModule
    {
        private static volatile HttpStatusCode HealthCheckStatus = HttpStatusCode.OK;

        public HealthCheckModule()
        {
            Get("/health", _ => Response.AsJson(
                model: new
                {
                    status = HealthCheckStatus,
                    details = HealthCheckStatus == HttpStatusCode.OK ? "All is going well." : "Oh no! I'm dead."
                },
                statusCode: HealthCheckStatus));

            // Force this instance to report that it's unhealthy
            Get("/die", _ =>
            {
                HealthCheckStatus = HttpStatusCode.ServiceUnavailable;
                return Response.AsJson("This instance will now report an unhealthy status on its /health endpoint.");
            });
        }
    }
}
