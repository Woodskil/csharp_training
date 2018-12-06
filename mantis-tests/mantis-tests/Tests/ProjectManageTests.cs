using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests.Tests
{
    [TestFixture]
    public class ProjectManageTests : AuthTestBase
    {
        [Test]
        public void CreateNewProjectTest()
        {
            //List<ProjectData> oldProjects = applicationManager.ProjectManagement.GetProjectList();
            List<ProjectData> oldProjects = applicationManager.API.GetAllProjects(admin);

            ProjectData project = new ProjectData()
            {
                Name = "new_test_project" + GenerateRandomInt(50),
                Description = "Description_for_test"
            };
            bool expectedResult = true;
            if (oldProjects.FindAll(p => p.Name == project.Name).Count > 0) { expectedResult = false; }

            bool result = applicationManager.ProjectManagement.AddNewProject(project);
            Assert.AreEqual(expectedResult, result);

            //List<ProjectData> newprojects = applicationManager.ProjectManagement.GetProjectList();
            List<ProjectData> newprojects = applicationManager.API.GetAllProjects(admin);
            if (result == true) { oldProjects.Add(project); }
            oldProjects.Sort();
            newprojects.Sort();
            Assert.AreEqual(oldProjects, newprojects);
        }

        [Test]
        public void RemoveProjectTest()
        {
            int indexOfRemoveProject = 0;

            //List<ProjectData> oldProjects = applicationManager.ProjectManagement.GetProjectList();
            List<ProjectData> oldProjects = applicationManager.API.GetAllProjects(admin);
            while (oldProjects.Count <= indexOfRemoveProject)
            {
                applicationManager.API.CreateNewProject(admin, new ProjectData("API_test_project_" + GenerateRandomInt(1000000), "API_description"));
                oldProjects = applicationManager.API.GetAllProjects(admin);
            }

            ProjectData project = oldProjects[indexOfRemoveProject];

            bool result = applicationManager.ProjectManagement.RemoveProject(project);
            Assert.AreEqual(true, result);

            //List<ProjectData> newprojects = applicationManager.ProjectManagement.GetProjectList();
            List<ProjectData> newprojects = applicationManager.API.GetAllProjects(admin);
            oldProjects.Remove(project);
            oldProjects.Sort();
            newprojects.Sort();
            Assert.AreEqual(oldProjects, newprojects);
        }
    }
}
