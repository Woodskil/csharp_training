﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            navigationHelper.OpenHomePage();
            loginHelper.Login(new AccountData("admin", "secret"));
            contactHelper.InitContactCreation();

            ContactData contact = new ContactData("Homer", "Simpson");
            contactHelper.FillContactForm(contact);

            contactHelper.SubmitContactCreation();
            loginHelper.Logout();
        }
    }
}
