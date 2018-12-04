using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    public class TestBase
    {
        public static bool PERFORM_LONG_UI_CHECKS = true;
        public static AccountData admin = new AccountData
        {
            Username = "administrator",
            Password = "password",
            Email = "root@localhost"
        };

        protected ApplicationManager applicationManager;

        [TestFixtureSetUp]
        public void SetupApplicationManager()
        {
            applicationManager = ApplicationManager.GetInstance();
        }

        public static Random rnd = new Random();

        public static string GenerateRandomString(int maxlength)
        {
            int length = Convert.ToInt32(rnd.NextDouble() * maxlength);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65)));
            }
            return builder.ToString();
        }

        public static int GenerateRandomInt(int maxInt)
        {
            return rnd.Next(maxInt);
        }
    }
}
