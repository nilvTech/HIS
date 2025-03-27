using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using HIS.APP.Data; // Ensure correct namespace for ApplicationDbContext
using HIS.APP.Models; // Ensure correct namespace for models
using Microsoft.EntityFrameworkCore;

namespace HIS.APP.Controllers
{
    public class HISController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<HISController> _logger;

        public HISController(ApplicationDbContext dbContext, ILogger<HISController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// PatientDemographics Action Method
        /// </summary>
        public IActionResult PatientDemographics()
        {
            try
            {
                var patientDemographics = _dbContext.Patientdemographics.AsNoTracking().ToList();
                return View(patientDemographics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving patient demographics.");
                return StatusCode(500, "Internal 
