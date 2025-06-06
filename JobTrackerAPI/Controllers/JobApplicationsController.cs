using JobTrackerAPI.Data;
using JobTrackerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using JobTrackerAPI.Data;
using JobTrackerAPI.Entities;
using Microsoft.EntityFrameworkCore;


namespace JobTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class JobApplicationsController : ControllerBase
    {
        //public class JobApplicationsController : ControllerBase
        
            private readonly JobDbContext _context;

            public JobApplicationsController(JobDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var jobs = await _context.Jobs.ToListAsync();
                return Ok(jobs);
            }

            [HttpPost]
            public async Task<IActionResult> Create(JobApplication job)
            {
                _context.Jobs.Add(job);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = job.Id }, job);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(Guid id)
            {
                var job = await _context.Jobs.FindAsync(id);
                return job == null ? NotFound() : Ok(job);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(Guid id, JobApplication updatedJob)
            {
                var job = await _context.Jobs.FindAsync(id);
                if (job == null) return NotFound();

                job.CompanyName = updatedJob.CompanyName;
                job.Position = updatedJob.Position;
                job.Role = updatedJob.Role;
                job.Status = updatedJob.Status;
                job.AppliedDate = updatedJob.AppliedDate;
                job.Notes = updatedJob.Notes;

                await _context.SaveChangesAsync();
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(Guid id)
            {
                var job = await _context.Jobs.FindAsync(id);
                if (job == null) return NotFound();

                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
                return NoContent();
            }

        }
    }
