
using AuditBenchmarkAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditBenchmarkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditBenchMarkController : ControllerBase
    {
        private readonly IBenchMarckRepo _context;

        public AuditBenchMarkController(IBenchMarckRepo context)
        {
            _context = context;
        }

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditBenchMarkController));

        [HttpGet("{auditType}")]
        public IActionResult GetInternalAndSOXCount(string auditType)
        {
            _log4net.Info("Get Internal And Sox Count Method Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var count = _context.GetInternaAndSOXNoCount(auditType);
                if (count == null)
                {
                    return NotFound();
                }
                return Ok(count);
            }
            catch (Exception )
            {

                return BadRequest();
            }
            
        }
    }
}
