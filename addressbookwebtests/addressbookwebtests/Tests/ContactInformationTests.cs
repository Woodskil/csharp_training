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
        public void ContactInformationTest()
        {
            int NumberOfContactWithInformation = 0;

            if (!applicationManager.PubContactHelper.ThereIsContact())
            {
                applicationManager.PubContactHelper.Create(new ContactData("test_last_name", "test_first_name"));
            }

            ContactData fromTable = applicationManager.PubContactHelper.GetContactInformationFromTable(NumberOfContactWithInformation);
            ContactData fromEdit = applicationManager.PubContactHelper.GetContactInformationFromEditForm(NumberOfContactWithInformation);

            Assert.AreEqual(fromTable, fromEdit);
            Assert.AreEqual(fromTable.Address, fromEdit.Address);
            Assert.AreEqual(fromTable.AllEmails, fromEdit.AllEmails);
            Assert.AreEqual(fromTable.AllPhones, fromEdit.AllPhones);
        }
    }
}
