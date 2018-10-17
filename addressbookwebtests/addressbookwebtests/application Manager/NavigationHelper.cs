using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
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
            driver.Navigate().GoToUrl(baseURL);
            return this;
        }

        public NavigationHelper GoToGroupsPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
            return this;
        }
    }
}
