using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS.APP.Data; // Ensure correct namespace for ApplicationDbContext
using HIS.APP.Models; // Ensure correct namespace for models
using Microsoft.EntityFrameworkCore;

namespace HIS.APP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HISController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<HISController> _logger;

        public HISController(ApplicationDbContext dbContext, ILogger<HISController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all patient demographics asynchronously.
        /// </summary>
        [HttpGet("patient-demographics")]
        public async Task<IActionResult> GetPatientDemographics()
        {
            try
            {
                var patientDemographics = await _dbContext.Patientdemographics.AsNoTracking().ToListAsync();
                return Ok(patientDemographics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving patient demographics.");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Retrieves a single patient demographic by ID.
        /// </summary>
        [HttpGet("patient-demographics/{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            try
            {
                var patient = await _dbContext.Patientdemographics.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                if (patient == null)
                {
                    return NotFound($"Patient with ID {id} not found.");
                }
                return Ok(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving patient with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Adds a new patient demographic record.
        /// </summary>
        [HttpPost("patient-demographics")]
        public async Task<IActionResult> CreatePatient([FromBody] PatientDemographics patient)
        {
            if (patient == null)
            {
                return BadRequest("Patient object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _dbContext.Patientdemographics.AddAsync(patient);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new patient record.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
