using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void ContactInformationTestFromHomeAndEdit()
        {
            int NumberOfContactWithInformation = 0;

            applicationManager.PubContactHelper.ChackOrCreateContact(NumberOfContactWithInformation);

            ContactData fromTable = applicationManager.PubContactHelper.GetContactInformationFromTable(NumberOfContactWithInformation);
            ContactData fromEdit = applicationManager.PubContactHelper.GetContactInformationFromEditForm(NumberOfContactWithInformation);

            Assert.AreEqual(fromTable, fromEdit);
            Assert.AreEqual(fromTable.Address, fromEdit.Address);
            Assert.AreEqual(fromTable.AllEmails, fromEdit.AllEmails);
            Assert.AreEqual(fromTable.AllPhones, fromEdit.AllPhones);
        }

        [Test]
        public void ContactInformationTestFromViewAndEdit()
        {
            int NumberOfContactWithInformation = 0;

            applicationManager.PubContactHelper.ChackOrCreateContact(NumberOfContactWithInformation);

            ContactData fromEdit = applicationManager.PubContactHelper.GetContactInformationFromEditForm(NumberOfContactWithInformation);
            string stringFromEdit = fromEdit.GetInformationFromViewForm();
            string stringFromView = applicationManager.PubContactHelper.GetContactInformationFromViewForm(NumberOfContactWithInformation);

            Assert.AreEqual(stringFromEdit, stringFromView);
        }

    }
}
