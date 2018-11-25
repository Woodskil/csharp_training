using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            int GroupNumber = 1;

            while (!applicationManager.PubGroupHelper.ThereIsGroup(GroupNumber))
            {
                applicationManager.PubGroupHelper.Create(new GroupData("test_group"));
            }

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData ToBeRemove = oldGroups[GroupNumber - 1];

            applicationManager.PubGroupHelper.Remove(ToBeRemove);
            applicationManager.PubGroupHelper.Wait(100);
            
            List<GroupData> newGroups = GroupData.GetAll();

            oldGroups.RemoveAt(GroupNumber - 1);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, ToBeRemove.Id);
            }
        }
    }
}
