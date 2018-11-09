using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            int GroupNumber = 1;

            while (!applicationManager.PubGroupHelper.ThereIsGroup(GroupNumber))
            {
                applicationManager.PubGroupHelper.Create(new GroupData("test_group"));
            }

            List<GroupData> oldGroups = applicationManager.PubGroupHelper.GetGroupList();
            GroupData RemovedGroup = oldGroups[GroupNumber - 1];

            applicationManager.PubGroupHelper.Remove(GroupNumber);
            applicationManager.PubGroupHelper.Wait(100);
            
            List<GroupData> newGroups = applicationManager.PubGroupHelper.GetGroupList();

            oldGroups.RemoveAt(GroupNumber - 1);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, RemovedGroup.Id);
            }
        }
    }
}
