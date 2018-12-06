using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager)
        {
            this.manager = manager;
            driver = manager.Driver;
        }

        public List<ProjectData> GetProjectList()
        {
            manager.Navigation.OpenManageProjectPage();
            List<ProjectData> projects = new List<ProjectData>();
            IList<IWebElement> elements = driver.FindElements(By.XPath("//tbody"))[0].FindElements(By.XPath("(.//tr)"));
            foreach (IWebElement element in elements)
            {
                var cells = element.FindElements(By.CssSelector("td"));
                projects.Add(new ProjectData(cells[0].Text, cells[4].Text));
            }
            return projects;
        }

        public bool AddNewProject(ProjectData project)
        {
            manager.Navigation.OpenManageProjectPage();
            driver.FindElements(By.XPath("//button[@type='submit']"))[0].Click();
            Type(By.Id("project-name"), project.Name);
            Type(By.Id("project-description"), project.Description);
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
            Wait(100);
            return !IsElementPresent(By.XPath("//div[@class='alert alert-danger']"));
        }

        public bool RemoveProject(ProjectData project)
        {
            manager.Navigation.OpenManageProjectPage();
            OpenProjectForEdit(project.Name);
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
            return !IsElementPresent(By.XPath("//div[@class='alert alert-danger']"));
        }

        public bool OpenProjectForEdit(string name)
        {
            manager.Navigation.OpenManageProjectPage();
            if (!IsElementPresent(By.XPath("(.//a[.='" + name + "'])"))){
                return false;
            }
            driver.FindElements(By.XPath("//tbody"))[0].FindElement(By.XPath("(.//a[.='" + name + "'])")).Click();
            return true;
        }
    }
}
