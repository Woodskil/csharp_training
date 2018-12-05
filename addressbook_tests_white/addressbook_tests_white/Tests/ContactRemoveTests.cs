using System;
using System.Collections.Generic;
using NUnit.Framework;


namespace addressbook_tests_white
{
    [TestFixture]
    public class ContactRemoveTests : TestBase
    {
        [Test]
        public void ContactRemoveTest()
        {
            int indexRemovedContact = 0;
            List<ContactData> oldContacts = app.Contacts.GetContactsList();
            while (oldContacts.Count <= indexRemovedContact)
            {
                app.Contacts.AddNewContact(new ContactData("test_last_name", "test_first_name"));
                oldContacts = app.Contacts.GetContactsList();
            }

            ContactData contact = oldContacts[indexRemovedContact];

            app.Contacts.RemoveContact(contact);

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Remove(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
