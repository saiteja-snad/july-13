using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SMS.HealthCheckDemo
{
    public class CustomeHealthCheck:IHealthCheck
    {


        public Task<HealthCheckResult> CheckHealthAsync(
               HealthCheckContext context,
               CancellationToken cancellationToken = default)
        {
            try
            {
                // Verify some service/resource

                return Task.FromResult(
                    HealthCheckResult.Healthy("Service is available."));
            }
            catch (Exception ex)
            {
                return Task.FromResult(
                    HealthCheckResult.Unhealthy(
                        "Service is unavailable.",
                        ex));
            }

        }
        }
    }
