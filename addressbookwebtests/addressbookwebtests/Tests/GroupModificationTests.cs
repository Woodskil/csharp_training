using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            int GroupNumber = 1;

            while (!applicationManager.PubGroupHelper.ThereIsGroup(GroupNumber))
            {
                applicationManager.PubGroupHelper.Create(new GroupData("test_group"));
            }

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData ModifiersGroup = oldGroups[GroupNumber - 1];

            GroupData newData = new GroupData("mewgroup");
            newData.Header = "newheader";
            newData.Footer = "newfooter";

            applicationManager.PubGroupHelper.Modify(ModifiersGroup, newData);
            applicationManager.PubGroupHelper.Wait(100);

            List<GroupData> newGroups = GroupData.GetAll();

            oldGroups[GroupNumber - 1] = newData;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == ModifiersGroup.Id)
                {
                    Assert.AreEqual(group.Name, newData.Name);
                }
            }
        }
    }
}
