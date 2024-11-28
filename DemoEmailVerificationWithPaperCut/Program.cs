using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var smtpSettings = builder.Configuration.GetSection("SmtpSettings");
builder.Services.AddFluentEmail(
    smtpSettings["FromEmail"],
    smtpSettings["FromName"])
    .AddSmtpSender(
    smtpSettings["Host"],
    smtpSettings.GetValue<int>("Port"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
