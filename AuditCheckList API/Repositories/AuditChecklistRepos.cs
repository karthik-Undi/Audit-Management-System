using AuditCheckListAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditCheckListAPI.Repositories
{

    public class AuditChecklistRepos : IAuditChecklistRepos
    {


        static List<QuestionsAndType> questionsAndTypes = new List<QuestionsAndType>()
        {
            new QuestionsAndType(){ Questions = "1. Have all Change requests followed SDLC before PROD move?" , AuditType = "Internal"},
            new QuestionsAndType() { Questions = "2. Have all Change requests been approved by the application owner?", AuditType = "Internal" },
            new QuestionsAndType() { Questions = "3. Are all artifacts like CR document, Unit test cases available?", AuditType = "Internal" },
            new QuestionsAndType() { Questions = "4. Is the SIT and UAT sign-off available?", AuditType = "Internal" },
            new QuestionsAndType() { Questions = "5. Is data deletion from the system done with application owner approval?", AuditType = "Internal" },
            new QuestionsAndType() { Questions = "1. Have all Change requests followed SDLC before PROD move?", AuditType = "SOX" },
            new QuestionsAndType() { Questions = "2. Have all Change requests been approved by the application owner?", AuditType = "SOX" },
            new QuestionsAndType() { Questions = "3. For a major change, was there a database backup taken before and after PROD move?", AuditType = "SOX" },
            new QuestionsAndType() { Questions = "4. Has the application owner approval obtained while adding a user to the system?", AuditType = "SOX" },
            new QuestionsAndType() { Questions = "5. Is data deletion from the system done with application owner approval?", AuditType = "SOX" }
        };


        public AuditChecklistRepos(List<QuestionsAndType> questionsAndTypeslist)
        {
            questionsAndTypes = questionsAndTypeslist;
        }

        public AuditChecklistRepos()
        {

        }
        public List<QuestionsAndType> AuditChecklistQuestions(string auditType)
        {
            return questionsAndTypes.Where(questions => questions.AuditType == auditType).ToList();
        }

        

    }


}
