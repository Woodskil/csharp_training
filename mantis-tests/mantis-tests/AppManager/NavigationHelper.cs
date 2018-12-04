using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class NavigationHelper : HelperBase
    {
        private string baseURL;

        public NavigationHelper(ApplicationManager manager) : base(manager)
        {
            baseURL = manager.BaseURL;
        }

        public NavigationHelper OpenHomePage()
        {
            if (driver.Url == baseURL + "my_view_page.php")
            {
                return this;
            }
            driver.Navigate().GoToUrl(baseURL + "my_view_page.php");
            return this;
        }

        public NavigationHelper OpenManageProjectPage()
        {
            if (driver.Url == baseURL + "manage_proj_page.php")
            {
                return this;
            }
            driver.Navigate().GoToUrl(baseURL + "manage_proj_page.php");
            return this;
        }

        public NavigationHelper OpenLoginPage()
        {
            if (driver.Url == baseURL + "login_page.php")
            {
                return this;
            }
            driver.Navigate().GoToUrl(baseURL + "login_page.php");
            return this;
        }

        public NavigationHelper OpenRegistrationPage()
        {
            if (driver.Url == baseURL + "signup_page.php")
            {
                return this;
            }
            driver.Navigate().GoToUrl(baseURL + "signup_page.php");
            return this;
        }

        public NavigationHelper OpenRegistrationPageUsingButton()
        {
            OpenLoginPage();
            driver.FindElement(By.XPath("//a[contains(@href, 'signup_page.php')]")).Click();
            return this;
        }

        public NavigationHelper OpenLink(string url)
        {
            driver.Navigate().GoToUrl(url);
            return this;
        }
    }
}
