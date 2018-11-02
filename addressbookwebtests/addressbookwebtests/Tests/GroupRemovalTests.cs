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
            List<GroupData> oldGroups = applicationManager.PubGroupHelper.GetGroupList();

            while (oldGroups.Count <= GroupNumber)
            {
                applicationManager.PubGroupHelper.Create(new GroupData("test_group"));
                oldGroups.Add(new GroupData("test_group"));
            }

            applicationManager.PubGroupHelper.Remove(GroupNumber);
            applicationManager.PubGroupHelper.Wait(100);
            
            List<GroupData> newGroups = applicationManager.PubGroupHelper.GetGroupList();

            oldGroups.RemoveAt(GroupNumber - 1);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
