using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            int NumberOfContact = 0;

            applicationManager.PubContactHelper.ChackOrCreateContact(NumberOfContact);

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData ToBeRemove = oldContacts[NumberOfContact];

            applicationManager.PubContactHelper.Remove(ToBeRemove);

            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, ToBeRemove.Id);
            }
        }
    }
}
