using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace addressbookwebtests_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataType = args[0];
            int count = Convert.ToInt32(args[1]);
            StreamWriter writer = new StreamWriter(args[2]);
            string format = args[3];

            if (dataType == "group")
            {
                List<GroupData> data = new List<GroupData>();
                for (int i = 0; i < count; i++)
                {
                    data.Add(new GroupData(TestBase.GenerateRandomString(30), TestBase.GenerateRandomString(100), TestBase.GenerateRandomString(100)));
                }
                data.Add(new GroupData("", "", ""));
                data.Add(new GroupData("Normal_Group_Name", "Normal_Group_Heater", "Normal_Group_Footer"));

                if (format == "csv")
                {
                    writeGroupsToCsvFile(data, writer);
                }
                else if (format == "xml")
                {
                    writeGroupsToXmlFile(data, writer);
                }
                else if (format == "json")
                {
                    writeGroupsToJsonFile(data, writer);
                }
                else
                {
                    System.Console.Out.Write("Invalid format!\n");
                }
            }

            else if (dataType == "contacts")
            {
                List<ContactData> data = new List<ContactData>();
                for (int i = 0; i < count; i++)
                {
                    data.Add(new ContactData(TestBase.GenerateRandomString(10), TestBase.GenerateRandomString(10)));
                }
                data.Add(new ContactData("", ""));
                data.Add(new ContactData("Simpson", "Homer"));

                if (format == "csv")
                {
                    writeGroupsToCsvFile(data, writer);
                }
                else if (format == "xml")
                {
                    writeGroupsToXmlFile(data, writer);
                }
                else if (format == "json")
                {
                    writeGroupsToJsonFile(data, writer);
                }
                else
                {
                    System.Console.Out.Write("Invalid format!\n");
                }

            }
            else
            {
                System.Console.Out.Write("Invalid format dataType - first argument!\n");
            }

            writer.Close();
        }

        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name,
                    group.Header,
                    group.Footer));
            }
        }

        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

        static void writeGroupsToCsvFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                writer.WriteLine(String.Format("${0},${1}",
                    contact.Lastname,
                    contact.Firstname));
            }
        }

        static void writeGroupsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        static void writeGroupsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
