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
    public class GroupHelper : HelperBase
    {
        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        public GroupHelper Create(GroupData group)
        {
            manager.PubNavigationHelper.GoToGroupsPage();
            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            return this;
        }

        public GroupHelper Modify(int p, GroupData newData)
        {
            manager.PubNavigationHelper.GoToGroupsPage();

            while (!ThereIsGroup(p))
            {
                Create(new GroupData("test_group"));
                manager.PubNavigationHelper.GoToGroupsPage();
            }

            SelectGroup(p);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupsPage();
            return this;
        }

        public GroupHelper Remove(int p)
        {
            manager.PubNavigationHelper.GoToGroupsPage();

            while (!ThereIsGroup(p))
            {
                Create(new GroupData("test_group"));
                manager.PubNavigationHelper.GoToGroupsPage();
            }

            SelectGroup(p);
            RemoveGroup();
            ReturnToGroupsPage();
            return this;
        }

        public GroupHelper ReturnToGroupsPage()
        {
            if (IsElementPresent(By.XPath("//input[@name='new' @value='New group']")))
            {
                return this;
            }
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }

        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }

        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }

        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public bool ThereIsGroup()
        {
            if (! IsElementPresent(By.XPath("//input[@name='new' @value='New group']")))
            {
                manager.PubNavigationHelper.GoToGroupsPage();
            }
            return IsElementPresent(By.XPath("//input[@name='selected[]']"));
        }

        public bool ThereIsGroup(int index)
        {
            if (!IsElementPresent(By.XPath("//input[@name='new' @value='New group']")))
            {
                manager.PubNavigationHelper.GoToGroupsPage();
            }
            return IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + index + "]"));
        }
    }
}
