using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager) { }

        public void CreateNewIssue(AccountData account, ProjectData projectData, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = projectData.Id;

            client.mc_issue_add(account.Username, account.Password, issue);
        }

        public List<ProjectData> GetAllProjects (AccountData account)
        {
            List<ProjectData> result_projects = new List<ProjectData>();

            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData[] m_projects = client.mc_projects_get_user_accessible(account.Username, account.Password);
            foreach (Mantis.ProjectData project in m_projects)
            {
                result_projects.Add(new ProjectData()
                {
                    Name = project.name,
                    Id = project.id,
                    Description = project.description
                });
            }
            return result_projects;
        }

        public void CreateNewProject (AccountData account, ProjectData projectData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData project = new Mantis.ProjectData();
            project.name = projectData.Name;
            project.description = projectData.Description;
            client.mc_project_add(account.Username, account.Password, project);
        }
            
    }
}
