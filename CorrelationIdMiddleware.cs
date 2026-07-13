namespace SMS.HealthCheckDemo
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CorrelationHeader = "X-Correlation-ID";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the incoming request has a correlation ID
            if (!context.Request.Headers.TryGetValue(CorrelationHeader, out var correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
            }

            // Set the correlation ID in response header for client visibility
            context.Response.Headers[CorrelationHeader] = correlationId;

            // Store correlation ID in HttpContext for downstream access
            context.Items[CorrelationHeader] = correlationId;

            await _next(context);
        }
    }
}
