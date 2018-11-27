using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class RelationContactAndGroupTests : ContactTestBase
    {
        [Test]
        public void AddingContactToGroupTest()
        {
            int GroupNumber = 1;

            while (!applicationManager.PubGroupHelper.ThereIsGroup(GroupNumber))
            {
                applicationManager.PubGroupHelper.Create(new GroupData("test_group"));
            }

            GroupData group = GroupData.GetAll()[GroupNumber-1];
            List<ContactData> oldList = group.GetContacts();

            List<ContactData> nonInGroupContacts = ContactData.GetAll().Except(oldList).ToList();
            if (nonInGroupContacts.Count() == 0)
            {
                applicationManager.PubContactHelper.Create(new ContactData("test_last_name", "test_first_name"));
                nonInGroupContacts = ContactData.GetAll().Except(oldList).ToList();
            }
            ContactData contact = nonInGroupContacts.First();

            applicationManager.PubContactHelper.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);

        }

        [Test]
        public void DeleteContactFromGroupTest()
        {
            int GroupNumber = 0;

            while (!applicationManager.PubGroupHelper.ThereIsGroup(GroupNumber + 1))
            {
                applicationManager.PubGroupHelper.Create(new GroupData("test_group"));
            }

            GroupData group = GroupData.GetAll()[GroupNumber];
            List<ContactData> oldList = group.GetContacts();

            if (oldList.Count() == 0)
            {
                if (ContactData.GetAll().ToList().Count() == 0)
                {
                    applicationManager.PubContactHelper.Create(new ContactData("test_last_name", "test_first_name"));
                }
                applicationManager.PubContactHelper.AddContactToGroup(ContactData.GetAll().ToList().First(), group);
                oldList = group.GetContacts();
            }
            ContactData contact = oldList.First();

            applicationManager.PubContactHelper.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
