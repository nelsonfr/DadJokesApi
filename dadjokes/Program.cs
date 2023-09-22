using dadjokes.Clients;
using dadjokes.Middleware;
using Microsoft.Extensions.Configuration;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);
var policy = "AllowSpecificOrigins";

// Add services to the container.
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: policy,
					  policy =>
					  {
						  policy.WithOrigins("http://localhost:4200");
						  policy.AllowAnyHeader();
						  policy.AllowAnyMethod();
					  });

});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IDadJokesClient, DadJokesClient>();
builder.Services.AddTransient<IRestClient, RestClient>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.UseCors(policy);
app.Run();
