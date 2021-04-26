using AuthorizationAPI.Model;
using AuthorizationAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AuthorizationNunitTestProject
{
    public class Tests
    {
        string token ;


        List<Userdetails> userdetails = new List<Userdetails>();

        IQueryable<Userdetails> userdetailsData;

        Mock<DbSet<Userdetails>> mockSet;

        Mock<AuditManagementSystemContext> auditManagementSystemContext;
        [SetUp]
       public void Setup()
        {
            token = "Thesearemyprivatekeys";
            userdetails = new List<Userdetails>()
           {
               new Userdetails{Email="aaa",Password="111"}
           };
            userdetailsData = userdetails.AsQueryable();

            mockSet = new Mock<DbSet<Userdetails>>();

            mockSet.As<IQueryable<Userdetails>>().Setup(m => m.Provider).Returns(userdetailsData.Provider);
            mockSet.As<IQueryable<Userdetails>>().Setup(m => m.Expression).Returns(userdetailsData.Expression);
            mockSet.As<IQueryable<Userdetails>>().Setup(m => m.ElementType).Returns(userdetailsData.ElementType);
            mockSet.As<IQueryable<Userdetails>>().Setup(m => m.GetEnumerator()).Returns(userdetailsData.GetEnumerator());

            var p = new DbContextOptions<AuditManagementSystemContext>();
            auditManagementSystemContext = new Mock<AuditManagementSystemContext>(p);
            auditManagementSystemContext.Setup(x => x.Userdetails).Returns(mockSet.Object);
        }
        [Test]
        public void CheckTokenPass()
        {
            var authRepo = new AuthenticationManager(token, auditManagementSystemContext.Object);
            var authrepotoken = authRepo.Authenticate("aaa", "111");
            Assert.IsNotNull(authrepotoken);
        }

        [Test]
        public void CheckTokenFail()
        {
            var authRepo = new AuthenticationManager(token, auditManagementSystemContext.Object);
            var authrepotoken = authRepo.Authenticate("aaa", "1111");
            Assert.IsNull(authrepotoken);
        }

    }
}