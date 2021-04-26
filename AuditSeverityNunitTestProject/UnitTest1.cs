using AuditSeverityAPI.Models;
using AuditSeverityAPI.Models.ViewModels;
using AuditSeverityAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuditSeverityNunitTestProject
{
    public class Tests
    {

        List<Audit> auditdetails = new List<Audit>();

        IQueryable<Audit> auditdetailsData;

        Mock<DbSet<Audit>> mockSet;

        Mock<AuditManagementSystemContext> auditManagementSystemContext;


        [SetUp]
        public void Setup()
        {
           
            auditdetails = new List<Audit>()
           {
               new Audit{Auditid=101,ProjectName="Face Detection",ProjectManagerName="Manali",ApplicationOwnerName="Python Labs",AuditType="Internal",AuditDate=DateTime.Now,ProjectExecutionStatus="Green",RemedialActionDuration="No action needed",Userid=1}
           };
            auditdetailsData = auditdetails.AsQueryable();

            mockSet = new Mock<DbSet<Audit>>();

            mockSet.As<IQueryable<Audit>>().Setup(m => m.Provider).Returns(auditdetailsData.Provider);
            mockSet.As<IQueryable<Audit>>().Setup(m => m.Expression).Returns(auditdetailsData.Expression);
            mockSet.As<IQueryable<Audit>>().Setup(m => m.ElementType).Returns(auditdetailsData.ElementType);
            mockSet.As<IQueryable<Audit>>().Setup(m => m.GetEnumerator()).Returns(auditdetailsData.GetEnumerator());

            var p = new DbContextOptions<AuditManagementSystemContext>();
            auditManagementSystemContext = new Mock<AuditManagementSystemContext>(p);
            auditManagementSystemContext.Setup(x => x.Audit).Returns(mockSet.Object);
        }
        [Test]
        public void PostAudittest()
        {
            var authRepo = new AuditSeverityRepos(auditManagementSystemContext.Object);
            var compObj = authRepo.PostAudit(new AuditDetails(){ Auditid = 101, ProjectName = "Face Detection", ProjectManagerName = "Manali", ApplicationOwnerName = "Python Labs", AuditType = "SOX", CountOfNos=3, AuditDate = DateTime.Now, ProjectExecutionStatus = "Red", RemedialActionDuration = "Action to be taken in 1 week", Userid = 1 });

            Assert.IsNotNull(compObj);
        }

        
    }
}