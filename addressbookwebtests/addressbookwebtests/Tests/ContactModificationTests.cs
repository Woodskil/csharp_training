using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            int NumberOfContact = 0;

            applicationManager.PubContactHelper.ChackOrCreateContact(NumberOfContact);

            List<ContactData> oldContacts = applicationManager.PubContactHelper.GetContactList();

            ContactData newcontact = new ContactData("NonSimpson", "NonHomer");

            applicationManager.PubContactHelper.Modify(NumberOfContact, newcontact);
            applicationManager.PubContactHelper.Wait(100);

            List<ContactData> newContacts = applicationManager.PubContactHelper.GetContactList();

            oldContacts[NumberOfContact] = newcontact;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
