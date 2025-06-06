using System.Text.Json;
using JobTrackerAPI.Models;

namespace JobTrackerAPI.Services
{
    public class JobService
    {
        private readonly string _filePath = "jobs.json";
        private readonly List<JobApplication> _jobs = new();

        public JobService()
        {
            LoadFromFile();
        }

        public List<JobApplication> GetAll() => _jobs;

        public JobApplication? GetById(Guid id) =>
            _jobs.FirstOrDefault(j => j.Id == id);

        public JobApplication Add(JobApplication job)
        {
            job.Id = Guid.NewGuid();
            _jobs.Add(job);
            SaveToFile();
            return job;
        }

        public bool Delete(Guid id)
        {
            var job = GetById(id);
            if (job != null)
            {
                _jobs.Remove(job);
                SaveToFile();
                return true;
            }
            return false;
        }

        public bool Update(Guid id, JobApplication updatedJob)
        {
            var job = GetById(id);
            if (job == null) return false;

            job.CompanyName = updatedJob.CompanyName;
            job.Role = updatedJob.Role;
            job.Status = updatedJob.Status;
            job.AppliedDate = updatedJob.AppliedDate;
            job.Notes = updatedJob.Notes;

            SaveToFile();
            return true;
        }

        private void SaveToFile()
        {
            try
            {
                var json = JsonSerializer.Serialize(_jobs, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving jobs: {ex.Message}");
            }
        }

        private void LoadFromFile()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var json = File.ReadAllText(_filePath);
                    var loadedJobs = JsonSerializer.Deserialize<List<JobApplication>>(json);
                    if (loadedJobs != null && loadedJobs.Any())
                    {
                        _jobs.AddRange(loadedJobs);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading jobs: {ex.Message}");
            }

        }
    }
}

       
    