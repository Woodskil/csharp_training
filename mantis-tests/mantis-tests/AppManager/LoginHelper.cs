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
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager)
        {
            this.manager = manager;
            driver = manager.Driver;
        }   

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                return;
            }
            Type(By.Id("username"), account.Username);
            driver.FindElement(By.XPath("//input[@value='Войти']")).Click();
            Type(By.Id("password"), account.Password);
            driver.FindElement(By.XPath("//input[@value='Войти']")).Click();
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.CssSelector("span.user-info"));
        }
    }
}
