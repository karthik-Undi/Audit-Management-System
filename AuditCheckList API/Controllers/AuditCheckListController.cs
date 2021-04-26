using AuditCheckListAPI.Models;
using AuditCheckListAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditCheckListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditCheckListController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditCheckListController));
        private readonly IAuditChecklistRepos _context;

        public AuditCheckListController(IAuditChecklistRepos context) 
        {
            _context = context;
        } 
        [HttpGet("{auditType}")]
        public IActionResult AuditChecklistQuestions(string auditType)
        {
            _log4net.Info("Get Questions By AuditType Was Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var questions = _context.AuditChecklistQuestions(auditType);
                _log4net.Info("Questions for Audit Type " + auditType + " Was Called");
                if (questions == null)
                {
                    return NotFound();
                }
                return Ok(questions);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
