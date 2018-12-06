using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;

namespace mantis_tests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [TestFixtureSetUp]
        public void SetUpConfig()
        {
            applicationManager.Ftp.BackUpFile("/config_defaults_inc.php");
            using (Stream localFile = File.Open("config_defaults_inc.php", FileMode.Open))
            {
                applicationManager.Ftp.Upload("/config_defaults_inc.php", localFile);
            }
        }

        [Test]
        public void AccountRegistrationTest()
        {
            //string randomUsername = "testuser" + GenerateRandomInt(1000);

            List<AccountData> accounts = applicationManager.Admin.GetAllAccounts();

            AccountData account = new AccountData()
            {
                Username = "testuser",
                Password = "password",
                Email = "testuser@localhost.localdomain"
            };

            AccountData existingAccount = accounts.Find(x => x.Username == account.Username);
            if (existingAccount != null)
            {
                applicationManager.Admin.DeleteAccount(existingAccount);
            }

            applicationManager.James.Delete(account);
            applicationManager.James.Add(account);
            applicationManager.Registration.Register(account);
        }

        [TestFixtureTearDown]
        public void RestoreConfig()
        {
            applicationManager.Ftp.RestoreBackUpFile("/config_defaults_inc.php");
        }
    }
}
