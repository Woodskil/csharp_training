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
            List<ContactData> oldContacts = applicationManager.PubContactHelper.GetContactList();

            if (oldContacts.Count == 0)
            {
                applicationManager.PubContactHelper.Create(new ContactData("test_last_name", "test_first_name"));
                oldContacts = applicationManager.PubContactHelper.GetContactList();

            }

            applicationManager.PubContactHelper.Remove();
            applicationManager.PubContactHelper.Wait(100);

            List<ContactData> newContacts = applicationManager.PubContactHelper.GetContactList();

            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
