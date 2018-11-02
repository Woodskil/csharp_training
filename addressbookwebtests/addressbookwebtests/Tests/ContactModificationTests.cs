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
            List<ContactData> oldContacts = applicationManager.PubContactHelper.GetContactList();
            
            if (oldContacts.Count == 0)
            {
                applicationManager.PubContactHelper.Create(new ContactData("test_last_name", "test_first_name"));
                oldContacts = applicationManager.PubContactHelper.GetContactList();
            }

            ContactData newcontact = new ContactData("NonSimpson", "NonHomer");

            applicationManager.PubContactHelper.Modify(newcontact);
            applicationManager.PubContactHelper.Wait(100);

            List<ContactData> newContacts = applicationManager.PubContactHelper.GetContactList();

            oldContacts[0] = newcontact;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
