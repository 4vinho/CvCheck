using CvCheck.Backend.Configuration;
using CvCheck.Backend.DependencyInjection;
using CvCheck.Backend.ExceptionHandling;

var builder = WebApplication.CreateBuilder(args);

var apiMetadata = builder.Configuration
    .GetSection(ApiMetadataOptions.SectionName)
    .Get<ApiMetadataOptions>() ?? new ApiMetadataOptions();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddOpenApi("v1");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBackendServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = "docs";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{apiMetadata.Name} {apiMetadata.Version}");
    });
}

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

public partial class Program;
