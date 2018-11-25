using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            int NumberOfContact = 0;

            applicationManager.PubContactHelper.ChackOrCreateContact(NumberOfContact);

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData ToBeChange = oldContacts[NumberOfContact];

            ContactData newcontact = new ContactData("NonSimpson", "NonHomer");

            applicationManager.PubContactHelper.Modify(ToBeChange, newcontact);

            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts[NumberOfContact] = newcontact;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == ToBeChange.Id)
                {
                    Assert.AreEqual(contact.Firstname, newcontact.Firstname);
                    Assert.AreEqual(contact.Lastname, newcontact.Lastname);
                }
            }
        }
    }
}
