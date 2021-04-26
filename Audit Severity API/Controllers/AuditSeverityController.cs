using AuditSeverityAPI.Models.ViewModels;
using AuditSeverityAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AuditSeverityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditSeverityController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditSeverityController));
        private readonly IAuditSeverityRepos _context;
        string _baseUrlForAudityBenchmarkApi = "http://localhost:60846/";

        public AuditSeverityController(IAuditSeverityRepos context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostAudit(AuditDetails item)
        {
            _log4net.Info("Post Audit Was Called !!");
            Dictionary<string, int> auditBenchmark = new Dictionary<string, int>();
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                    using(var httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri(_baseUrlForAudityBenchmarkApi);
                        httpClient.DefaultRequestHeaders.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage Res = await httpClient.GetAsync("api/AuditBenchMark/" + item.AuditType);
                        if(Res.IsSuccessStatusCode)
                        {
                            _log4net.Info("Audit Benchmark For Audit Type " + item.AuditType + " Was Called !!");
                            var Response = Res.Content.ReadAsStringAsync().Result;
                            auditBenchmark = JsonConvert.DeserializeObject<Dictionary<string, int>>(Response);
                        }
                    }
                int CountOfNosAllowed = auditBenchmark[item.AuditType];
                if (item.AuditType == "Internal")
                {
                    if (item.CountOfNos > CountOfNosAllowed)
                    {
                        item.ProjectExecutionStatus = "RED";
                        item.RemedialActionDuration = "Action to be taken in 2 weeks";
                    }
                    else
                    {
                        item.ProjectExecutionStatus = "GREEN";
                        item.RemedialActionDuration = "No action needed";
                    }

                }
                else
                {
                    if (item.AuditType == "SOX")
                    {
                        if (item.CountOfNos > CountOfNosAllowed)
                        {
                            item.ProjectExecutionStatus = "RED";
                            item.RemedialActionDuration = "Action to be taken in 1 week";
                        }
                        else
                        {
                            item.ProjectExecutionStatus = "GREEN";
                            item.RemedialActionDuration = "No action needed";
                        }
                    }
                }
                var addAudit = await _context.PostAudit(item);
                return Ok(addAudit);

            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}
