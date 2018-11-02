using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Simpson", "Homer");

            List<ContactData> oldContacts = applicationManager.PubContactHelper.GetContactList();

            applicationManager.PubContactHelper.Create(contact);
            applicationManager.PubContactHelper.Wait(100);

            List<ContactData> newContacts = applicationManager.PubContactHelper.GetContactList();

            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
