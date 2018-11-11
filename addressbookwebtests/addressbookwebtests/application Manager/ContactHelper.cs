using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.PubNavigationHelper.GoToContactsPage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));

            return new ContactData()
            {
                Lastname = cells[1].Text,
                Firstname = cells[2].Text,
                Address = cells[3].Text,
                AllEmails = cells[4].Text,
                AllPhones = cells[5].Text
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.PubNavigationHelper.GoToContactsPage();
            InitContactModify(index);
            return ReadContactFromEditForm();
        }

        public ContactHelper Create(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            return this;
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.PubNavigationHelper.GoToContactsPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name=entry]"));
                foreach (IWebElement element in elements)
                {
                    var cells = element.FindElements(By.CssSelector("td"));
                    contactCache.Add(new ContactData(cells[1].Text, cells[2].Text));
                }
            }
            return new List<ContactData>(contactCache);
        }

        public ContactHelper Modify(ContactData newcontact)
        {
            manager.PubNavigationHelper.GoToContactsPage();
            InitContactModify();
            FillContactForm(newcontact);
            SubmitContactUpdating();
            return this;
        }

        public ContactHelper Remove()
        {
            manager.PubNavigationHelper.GoToContactsPage();
            SelectContact();
            Wait(100);
            RemoveContact();
            return this;
        }

        public ContactData ReadContactFromEditForm()
        {
            return new ContactData()
            {
                Lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value"),
                Firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value"),
                Middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value"),
                Nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value"),
                Address = driver.FindElement(By.Name("address")).GetAttribute("value"),
                HomePhone = driver.FindElement(By.Name("home")).GetAttribute("value"),
                MobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value"),
                WorkPhone = driver.FindElement(By.Name("work")).GetAttribute("value"),
                Email = driver.FindElement(By.Name("email")).GetAttribute("value"),
                Email2 = driver.FindElement(By.Name("email2")).GetAttribute("value"),
                Email3 = driver.FindElement(By.Name("email3")).GetAttribute("value")
            };
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
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
            contactCache = null;
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
            contactCache = null;
            return this;
        }

        public ContactHelper SubmitContactUpdating()
        {
            driver.FindElement(By.XPath("//input[@name='update']")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModify(int index = 0)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public bool ThereIsContact()
        {
            if (! (IsElementPresent(By.XPath("//input[@name='searchstring']")) && IsElementPresent(By.XPath("//input[@value='Send e-Mail']"))) )
            {
                manager.PubNavigationHelper.GoToContactsPage();
            }
            return IsElementPresent(By.Name("entry"));
        }

        public int GetNumberOfSearchResult()
        {
            manager.PubNavigationHelper.GoToContactsPage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match reg_march = new Regex(@"\d+").Match(text);
            return Int32.Parse(reg_march.Value); 
        }
    }
}
