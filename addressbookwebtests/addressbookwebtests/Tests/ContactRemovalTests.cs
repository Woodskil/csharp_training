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
            int NumberOfContact = 0;

            applicationManager.PubContactHelper.ChackOrCreateContact(NumberOfContact);

            List<ContactData> oldContacts = applicationManager.PubContactHelper.GetContactList();

            applicationManager.PubContactHelper.Remove(NumberOfContact);

            applicationManager.PubContactHelper.Wait(500);
            List<ContactData> newContacts = applicationManager.PubContactHelper.GetContactList();

            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
