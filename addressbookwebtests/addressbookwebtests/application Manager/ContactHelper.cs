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

        public ContactHelper AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.PubNavigationHelper.GoToContactsPage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            return this;
        }

        public ContactHelper RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.PubNavigationHelper.GoToContactsPage();
            OpenGroupFilter(group.Id);
            SelectContact(contact.Id);
            CommitRemoveFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            return this;
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public void OpenGroupFilter(string group_id)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByValue(group_id);
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void CommitRemoveFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        public string GetContactInformationFromViewForm(int index)
        {
            manager.PubNavigationHelper.GoToViewContactsPage(index);
            string info = driver.FindElement(By.Id("content")).Text;

            return info;
        }

        public ContactHelper Create(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
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
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            return this;
        }

        public ContactHelper Modify(int index, ContactData newcontact)
        {
            manager.PubNavigationHelper.GoToContactsPage();
            InitContactModify(index);
            FillContactForm(newcontact);
            SubmitContactUpdating();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            return this;
        }

        public ContactHelper Modify(ContactData toBeChange, ContactData newcontact)
        {
            manager.PubNavigationHelper.GoToContactsPage();
            InitContactModify(toBeChange.Id);
            FillContactForm(newcontact);
            SubmitContactUpdating();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            return this;
        }

        public ContactHelper Remove( int index = 0)
        {
            manager.PubNavigationHelper.GoToContactsPage();
            SelectContact(index);
            Wait(100);
            RemoveContact();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            return this;
        }

        public ContactHelper Remove(ContactData toBeRemoved)
        {
            manager.PubNavigationHelper.GoToContactsPage();
            SelectContact(toBeRemoved.Id);
            RemoveContact();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
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

        public ContactHelper SelectContact(int index)
        {
            driver.FindElements(By.Name("entry"))[index].FindElement(By.XPath("(//input[@name='selected[]'])")).Click();
            return this;
        }

        public ContactHelper SelectContact(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
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
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper InitContactModify(string id)
        {
            driver.FindElement(By.XPath("//a[contains(@href, 'edit.php?id=" + id + "')]")).Click();
            return this;
        }

        public int GetNumberOfSearchResult()
        {
            manager.PubNavigationHelper.GoToContactsPage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match reg_march = new Regex(@"\d+").Match(text);
            return Int32.Parse(reg_march.Value); 
        }

        public bool ThereIsContact()
        {
            manager.PubNavigationHelper.GoToContactsPage();
            return IsElementPresent(By.Name("entry"));
        }

        public bool ThereIsContact(int index)
        {
            manager.PubNavigationHelper.GoToContactsPage();
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name=entry]"));
            return elements.Count > index;
        }

        public ContactHelper ChackOrCreateContact(int index = 0)
        {
            while (!ThereIsContact(index))
            {
                Create(new ContactData("test_last_name", "test_first_name"));
            }
            return this;
        }
    }
}
