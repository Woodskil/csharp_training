using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();

            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30), GenerateRandomString(100), GenerateRandomString(100)));
            }

            groups.Add(new GroupData("", "", ""));
            groups.Add(new GroupData("Normal_Group_Name", "Normal_Group_Heater", "Normal_Group_Footer"));

            return groups;
        }

        [Test, TestCaseSource("RandomGroupDataProvider")]
        public void GroupCreationTest(GroupData group)
        {
            List<GroupData> oldGroups = applicationManager.PubGroupHelper.GetGroupList();

            applicationManager.PubGroupHelper.Create(group);
            applicationManager.PubGroupHelper.Wait(100);

            List<GroupData> newGroups = applicationManager.PubGroupHelper.GetGroupList();

            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
