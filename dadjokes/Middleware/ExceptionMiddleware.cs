namespace dadjokes.Middleware
{
	public class ExceptionMiddleware : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{

				await next.Invoke(context);
			}
			catch (Exception ex)
			{

				context.Response.StatusCode = 500; // Internal Server Error
				context.Response.ContentType = "application/json";

				// Create a custom error response
				var errorMessage = new
				{
					Message = "An error occurred",
					ErrorDetails = ex.Message
				};

				await context.Response.WriteAsJsonAsync(errorMessage);
			}
		}
	}
}
