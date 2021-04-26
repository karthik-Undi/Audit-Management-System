using AuditCheckListAPI.Models;
using AuditCheckListAPI.Repositories;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AuditCheckListTestProject
{
    public class Tests
    {
        List<QuestionsAndType> questions = new List<QuestionsAndType>();
        IQueryable<QuestionsAndType> questionsdata;


        [SetUp]
        public void Setup()
        {
            questions = new List<QuestionsAndType>()
            {
                new QuestionsAndType{Questions="Have all Change requests followed SDLC before PROD move?", AuditType="Internal"},
                new QuestionsAndType() { Questions = "2. Have all Change requests been approved by the application owner?", AuditType = "Internal" },
                new QuestionsAndType() { Questions = "3. Are all artifacts like CR document, Unit test cases available?", AuditType = "Internal" },
                new QuestionsAndType() { Questions = "1. Have all Change requests followed SDLC before PROD move?", AuditType = "SOX" },
                new QuestionsAndType() { Questions = "2. Have all Change requests been approved by the application owner?", AuditType = "SOX" }


            };
            questionsdata = questions.AsQueryable();
            

        }

        


        [Test]
        public void GetAllChecklistQuestionsListInternalTest()
        {

            var compRepo = new AuditChecklistRepos(questions);
            var compList = compRepo.AuditChecklistQuestions("Internal");
            Assert.AreEqual(3, compList.Count());
        }

        [Test]
        public void GetAllChecklistQuestionsListInternalTestFail()
        {

            var compRepo = new AuditChecklistRepos(questions);
            var compList = compRepo.AuditChecklistQuestions("Internal");
            Assert.AreEqual(5, compList.Count());
        }



        [Test]
        public void GetAllChecklistQuestionsListSOXTest()
        {

            var compRepo = new AuditChecklistRepos(questions);
            var compList = compRepo.AuditChecklistQuestions("SOX");
            Assert.AreEqual(2, compList.Count());
        }


        [Test]
        public void GetAllChecklistQuestionsListSOXTestFail()
        {

            var compRepo = new AuditChecklistRepos(questions);
            var compList = compRepo.AuditChecklistQuestions("SOX");
            Assert.AreEqual(5, compList.Count());
        }
    }

}