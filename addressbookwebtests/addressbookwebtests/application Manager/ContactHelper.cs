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
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            return this;
        }

        public ContactHelper Modify(ContactData newcontact)
        {
            manager.PubNavigationHelper.GoToContactsPage();

            if (!ThereIsContact())
            {
                Create(new ContactData("test_first_name", "test_middlename"));
                manager.PubNavigationHelper.GoToContactsPage();
            }

            InitContactModify();
            FillContactForm(newcontact);
            SubmitContactUpdating();
            return this;
        }

        public ContactHelper Remove()
        {
            manager.PubNavigationHelper.GoToContactsPage();

            if (!ThereIsContact())
            {
                Create(new ContactData("test_first_name", "test_middlename"));
                manager.PubNavigationHelper.GoToContactsPage();
            }

            SelectContact();
            RemoveContact();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public ContactHelper SelectContact()
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])")).Click();
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            return this;
        }

        public ContactHelper SubmitContactUpdating()
        {
            driver.FindElement(By.XPath("//input[@name='update']")).Click();
            return this;
        }

        public ContactHelper InitContactModify()
        {
            driver.FindElement(By.XPath("//img[@alt='Edit']")).Click();
            return this;
        }

        public bool ThereIsContact()
        {
            if (! (IsElementPresent(By.XPath("//input[@name='searchstring']")) && IsElementPresent(By.XPath("//input[@value='Send e-Mail']"))) )
            {
                manager.PubNavigationHelper.GoToContactsPage();
            }
            return IsElementPresent(By.Name("selected[]"));
        }
    }
}
