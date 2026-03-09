using System.Net;

namespace FlowerShop.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex, _env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, IHostEnvironment env)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; 

            var response = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = env.IsDevelopment() ? ex.Message : "Lỗi hệ thống máy chủ, vui lòng thử lại sau!",
                Details = env.IsDevelopment() ? ex.StackTrace?.ToString() : null
            };

            return context.Response.WriteAsync(response.ToString());
        }
    }
}
