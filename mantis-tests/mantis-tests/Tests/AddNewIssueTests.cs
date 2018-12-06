using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class AddNewIssueTests : TestBase
    {
        [Test]
        public void AddNewIssueTest()
        {
            IssueData issue = new IssueData()
            {
                Summary = "summary text",
                Description = "description text",
                Category = "General"
            };

            ProjectData project = new ProjectData()
            {
                Name = "new_test_project",
                Description = "Description_for_test",
                Id = "1"
            };

            applicationManager.API.CreateNewIssue(admin, project, issue);
        }
    }
}
