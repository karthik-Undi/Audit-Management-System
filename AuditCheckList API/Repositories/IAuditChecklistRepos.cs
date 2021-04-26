using AuditCheckListAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditCheckListAPI.Repositories
{
    public interface IAuditChecklistRepos
    {
        List<QuestionsAndType> AuditChecklistQuestions(string auditType);
    }
}
