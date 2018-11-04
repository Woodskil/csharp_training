using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            if (!applicationManager.PubContactHelper.ThereIsContact())
            {
                applicationManager.PubContactHelper.Create(new ContactData("test_last_name", "test_first_name"));
            }

            List<ContactData> oldContacts = applicationManager.PubContactHelper.GetContactList();

            applicationManager.PubContactHelper.Remove();

            applicationManager.PubContactHelper.Wait(200);
            List<ContactData> newContacts = applicationManager.PubContactHelper.GetContactList();

            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
