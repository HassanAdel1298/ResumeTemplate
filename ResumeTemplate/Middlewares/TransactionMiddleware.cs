
using ResumeTemplate.Data;

namespace ResumeTemplate.Middlewares
{
    public class TransactionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Context _context;

        public TransactionMiddleware(RequestDelegate next, Context context)
        {
            _next = next;
            _context = context;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var method = httpContext.Request.Method.ToUpper();
            if (method == "POST" || method == "PUT" || method == "DELETE")
            {
                var transaction = _context.Database.BeginTransaction();

                try
                {
                    await _next(httpContext);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                await _next(httpContext);
            }
        }
    }
}
