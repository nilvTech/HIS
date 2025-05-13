using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using HIS.APP.Data;
using HIS.APP.Models;
using Microsoft.EntityFrameworkCore;

namespace HIS.APP.Controllers
{
    [ApiController]
    [Route("api/[controller]/patient-demographics")]
    public class HISController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<HISController> _logger;

        public HISController(ApplicationDbContext dbContext, ILogger<HISController> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves all patient demographics.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var patients = await _dbContext.Patientdemographics
                                               .AsNoTracking()
                                               .ToListAsync(cancellationToken);
                return Ok(patients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve patient demographics.");
                return StatusCode(500, "An error occurred while retrieving data.");
            }
        }

        /// <summary>
        /// Retrieves a patient demographic by ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var patient = await _dbContext.Patientdemographics
                                              .AsNoTracking()
                                              .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (patient == null)
                {
                    return NotFound($"Patient with ID {id} was not found.");
                }

                return Ok(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve patient with ID {PatientId}.", id);
                return StatusCode(500, "An error occurred while retrieving the patient.");
            }
        }

        /// <summary>
        /// Creates a new patient demographic record.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PatientDemographics patient, CancellationToken cancellationToken = default)
        {
            if (patient == null)
            {
                return BadRequest("Patient data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _dbContext.Patientdemographics.AddAsync(patient, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return CreatedAtAction(nameof(GetByIdAsync), new { id = patient.Id }, patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create patient record.");
                return StatusCode(500, "An error occurred while saving the patient.");
            }
        }
    }
}
