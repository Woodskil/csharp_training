using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        protected LoginHelper loginHelper;
        protected NavigationHelper navigationHelper;
        protected GroupHelper groupHelper;
        protected ContactHelper contactHelper;

        private static ThreadLocal<ApplicationManager> applicationManager = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/addressbook/";

            loginHelper = new LoginHelper(this);
            navigationHelper = new NavigationHelper(this);
            groupHelper = new GroupHelper(this);
            contactHelper = new ContactHelper(this);
        }

        public static ApplicationManager GetInstance()
        {
            if (! applicationManager.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.PubNavigationHelper.OpenHomePage();
                applicationManager.Value = newInstance;
            }
            return applicationManager.Value;
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public string BaseURL
        {
            get
            {
                return baseURL;
            }
        }

        public LoginHelper PubLoginHelper
        {
            get
            {
                return loginHelper;
            }
        }

        public NavigationHelper PubNavigationHelper
        {
            get
            {
                return navigationHelper;
            }
        }

        public GroupHelper PubGroupHelper
        {
            get
            {
                return groupHelper;
            }
        }

        public  ContactHelper PubContactHelper
        {
            get
            {
                return contactHelper;
            }
        }

        ~ApplicationManager()
        {
            try
            {   
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
    }
}
