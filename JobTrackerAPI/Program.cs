using JobTrackerAPI.Data;
using JobTrackerAPI.Models;
using JobTrackerAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<JobService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();// Add Swagger (optional but helpful)
builder.Services.AddDbContext<JobDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JobTrackerDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
//builder.Services.AddDbContext<JobDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Add services to the container.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();



var jobService = app.Services.GetRequiredService<JobService>();

app.MapGet("/jobs", () => jobService.GetAll());

app.MapGet("/jobs/{id}", (Guid id) =>
{
    var job = jobService.GetById(id);
    return job is not null ? Results.Ok(job) : Results.NotFound();
});

app.MapPost("/jobs", (JobApplication job) =>
{
    var created = jobService.Add(job);
    return Results.Created($"/jobs/{created.Id}", created);
});

//app.MapPut("/jobs/{id}", (int id, JobApplication job) =>
//{
//    return jobService.Update(id, job) ? Results.NoContent() : Results.NotFound();
//});

app.MapPut("/jobs/{id}", (Guid id, JobApplication job) =>
{
    return jobService.Update(id, job) ? Results.NoContent() : Results.NotFound();
});



app.MapDelete("/jobs/{id}", (Guid id) =>
{
    return jobService.Delete(id) ? Results.NoContent() : Results.NotFound()
    ;
});

//filter/search option
app.MapGet("/jobs/search", (
    [FromQuery] string? status,
    [FromQuery] string? company,
    [FromQuery] string? role
) =>
{
    var jobs = jobService.GetAll();

    var results = jobs.Where(job =>
        (string.IsNullOrWhiteSpace(status) ||
         job.Status != null && job.Status.Equals(status, StringComparison.OrdinalIgnoreCase)) &&
        (string.IsNullOrWhiteSpace(company) ||
         job.CompanyName != null && job.CompanyName.ToLower().Contains(company.ToLower())) &&
        (string.IsNullOrWhiteSpace(role) ||
         job.Role != null && job.Role.ToLower().Contains(role.ToLower()))
    ).ToList();

    return Results.Ok(results);
});

//app.MapGet("/jobs/search", (string? status, string? company, string? role) =>
//{
//    var results = jobService.GetAll().Where(job =>
//        (string.IsNullOrEmpty(status) || job.Status.Equals(status, StringComparison.OrdinalIgnoreCase)) &&
//        (string.IsNullOrEmpty(company) || job.CompanyName.Contains(company, StringComparison.OrdinalIgnoreCase)) &&
//        (string.IsNullOrEmpty(role) || job.Role.Contains(role, StringComparison.OrdinalIgnoreCase))
//    ).ToList();

//    return results.Any() ? Results.Ok(results) : Results.NotFound("No matching jobs found.");
//});


//// Configure the HTTP request pipeline.
//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};


// API Endpoints
//app.MapGet("/", () => "🎯 Job Application Tracker API is running!");
app.Run();


