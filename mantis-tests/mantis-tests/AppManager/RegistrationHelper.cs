using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class RegistrationHelper : HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager)
        {
            this.manager = manager;
            driver = manager.Driver;
        }

        public void Register(AccountData account)
        {
            manager.Navigation.OpenRegistrationPageUsingButton();
            FillRegistrationForm(account);
            SubmitRegistration();
            manager.Navigation.OpenLink(GetConfirmUrlFromMail(account));
            FillPasswordDuringRegistration(account);
            SubmitPasswordForm();
        }

        private void FillRegistrationForm(AccountData account)
        {
            Type(By.Id("username"), account.Username);
            Type(By.Id("email-field"), account.Email);
        }

        private void SubmitRegistration()
        {
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }

        private string GetConfirmUrlFromMail(AccountData account)
        {
            string message = manager.Mail.GetLastMail(account);
            return Regex.Match(message, @"http://\S*").Value;
        }

        private void FillPasswordDuringRegistration(AccountData account)
        {
            Type(By.Id("password"), account.Password);
            Type(By.Id("password-confirm"), account.Password);     
        }   

        private void SubmitPasswordForm()
        {
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }
    }
}
