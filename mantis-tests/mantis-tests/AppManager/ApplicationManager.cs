using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        protected NavigationHelper navigationHelper;
        protected RegistrationHelper registrationHelper;
        protected FtpHelper ftpHelper;
        protected JamesHelper jamesHelper;
        protected MailHelper mailHelper;
        protected LoginHelper loginHelper;
        protected ProjectManagementHelper projectManagementHelper;
        protected AdminHelper adminHelper;
        protected APIHelper aPIHelper;

        private static ThreadLocal<ApplicationManager> applicationManager = new ThreadLocal<ApplicationManager>();

        public IWebDriver Driver { get { return driver; } }
        public string BaseURL { get { return baseURL; } }

        public NavigationHelper Navigation { get { return navigationHelper; } }
        public RegistrationHelper Registration { get { return registrationHelper; } }
        public FtpHelper Ftp { get { return ftpHelper; } }
        public JamesHelper James { get { return jamesHelper; } }
        public MailHelper Mail { get { return mailHelper; } }
        public LoginHelper Login { get { return loginHelper; } }
        public ProjectManagementHelper ProjectManagement { get { return projectManagementHelper; } }
        public AdminHelper Admin { get { return adminHelper; } }
        public APIHelper API { get { return aPIHelper; } }

        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/mantisbt-2.18.0/";

            navigationHelper = new NavigationHelper(this);
            registrationHelper = new RegistrationHelper(this);
            ftpHelper = new FtpHelper(this);
            jamesHelper = new JamesHelper(this);
            mailHelper = new MailHelper(this);
            loginHelper = new LoginHelper(this);
            projectManagementHelper = new ProjectManagementHelper(this);
            adminHelper = new AdminHelper(this, baseURL);
            aPIHelper = new APIHelper(this);
        }

        public static ApplicationManager GetInstance()
        {
            if (! applicationManager.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Navigation.OpenLoginPage();
                applicationManager.Value = newInstance;
            }
            return applicationManager.Value;
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
