namespace HOMEWORK_15__Doctor_Appointment_Tool.Controllers.Middleware
{
    public class BookingCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public BookingCheckMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            bool bookingNotAllowed = _configuration.GetValue<bool>("BookingNotAllowed");

            if (bookingNotAllowed &&
     (context.Request.Path.StartsWithSegments("/Appointment/Book") ||
      context.Request.Path == "/"))
            {
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Booking is currently unavailable. Please try again later.");
                return;             }

            await _next(context);
        }
    }
}
