using AuditSeverityAPI.Models;
using AuditSeverityAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityAPI.Repositories
{
    public class AuditSeverityRepos : IAuditSeverityRepos
    {
        private readonly AuditManagementSystemContext _context;

        public AuditSeverityRepos()
        {

        }

        public AuditSeverityRepos(AuditManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<Audit> PostAudit(AuditDetails item)
        {
            Audit audit = null;
            if(item == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                audit = new Audit()
                {
                    Auditid = item.Auditid,
                    ProjectName = item.ProjectName,
                    ProjectManagerName = item.ProjectManagerName,
                    ApplicationOwnerName = item.ApplicationOwnerName,
                    AuditType = item.AuditType,
                    AuditDate = DateTime.Now,
                    ProjectExecutionStatus = item.ProjectExecutionStatus,
                    RemedialActionDuration = item.RemedialActionDuration,
                    Userid = item.Userid
                };
                await _context.Audit.AddAsync(audit);
                await _context.SaveChangesAsync();
            }
            return audit;
        }
    }
}
