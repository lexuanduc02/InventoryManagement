namespace InventoryManagement.Middlewares
{
    public class NotFoundMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                // If a 404 error is detected, redirect the request to the NotFound action in the HomeController
                context.Request.Path = "/Home/NotFound";
                // Optionally, you might also want to clear any previous response content
                // context.Response.Clear();
            }

            // Continue processing the request pipeline
            await _next(context);
        }
    }
}
