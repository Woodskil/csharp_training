using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }
        [Column(Name = "lastname")]
        public string Lastname { get; set; }
        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "middlename")]
        public string Middlename { get; set; }
        [Column(Name = "nickname")]
        public string Nickname { get; set; }
        [Column(Name = "address")]
        public string Address { get; set; }
        [Column(Name = "home")]
        public string HomePhone { get; set; }
        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }
        [Column(Name = "work")]
        public string WorkPhone { get; set; }
        [Column(Name = "email")]
        public string Email { get; set; }
        [Column(Name = "email2")]
        public string Email2 { get; set; }
        [Column(Name = "email3")]
        public string Email3 { get; set; }

        private string allPhones;
        private string allEmails;

        [Column(Name ="deprecated")]
        public string Deprecated { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    string result = "";
                    if (Email != null && Email != "") { result += Email + "\r\n"; }
                    if (Email2 != null && Email2 != "") { result += Email2 + "\r\n"; }
                    if (Email3 != null && Email3 != "") { result += Email3; }

                    return result.Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

        public ContactData()
        {
        }

        public ContactData(string lastname, string firstname)
        {
            Lastname = lastname;
            Firstname = firstname;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return this.Lastname == other.Lastname && this.Firstname == other.Firstname;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return 0;
            }
            return ("" + this.Lastname + " " + this.Firstname).CompareTo("" + other.Lastname + " " + other.Firstname);
        }

        public override int GetHashCode()
        {
            return ("" + this.Lastname + " " + this.Firstname).GetHashCode();
        }

        public override string ToString()
        {
            return ("contact = " + this.Lastname + " " + this.Firstname);
        }

        private string GetInfoString(string str1, string str2, string str3, string str4 = null, string str5 = null)
        {
            string doneStr = null;

            if (str1 != null && str1 != "") { doneStr += str1 + "\r\n"; }
            if (str2 != null && str2 != "") { doneStr += str2 + "\r\n"; }
            if (str3 != null && str3 != "") { doneStr += str3 + "\r\n"; }
            if (str4 != null && str4 != "") { doneStr += str4 + "\r\n"; }
            if (str5 != null && str5 != "") { doneStr += str4 + "\r\n"; }
            if (doneStr != null && doneStr != "") { doneStr += "\r\n"; }

            return doneStr;
        }

        public string GetInformationFromViewForm()
        {
            string infoString = "";
            string fullname = null;
            string plusHomePhone = null;
            string plusMobilePhone = null;
            string plusWorkPhone = null;

            if (this.Firstname != null && this.Firstname != "")
            {
                fullname += this.Firstname;
            }
            if (this.Middlename != null && this.Middlename != "")
            {
                if (fullname != null && fullname != "")
                {
                    fullname += " ";
                }
                fullname += this.Middlename;
            }
            if (this.Lastname != null && this.Lastname != "")
            {
                if (fullname != null && fullname != "")
                {
                    fullname += " ";
                }
                fullname += this.Lastname;
            }

            if (this.HomePhone != null && this.HomePhone != "") { plusHomePhone = "H: " + this.HomePhone; }
            if (this.MobilePhone != null && this.MobilePhone != "") { plusMobilePhone = "M: " + this.MobilePhone; }
            if (this.WorkPhone != null && this.WorkPhone != "") { plusWorkPhone = "W: " + this.WorkPhone; }

            infoString += GetInfoString(fullname, this.Nickname, this.Address);
            infoString += GetInfoString(plusHomePhone, plusMobilePhone, plusWorkPhone);
            infoString += GetInfoString(this.Email, this.Email2, this.Email3);

            if (infoString.Length > 1)
            {
                while (infoString.Substring(infoString.Length - 2) == "\r\n")
                {
                    infoString = infoString.Substring(0, infoString.Length - 2);
                }
            }

            return infoString;
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }

        }

        public List<GroupData> GetGroups()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups
                        from gcr in db.GCR.Where(p => p.GroupId == g.Id && p.ContactId == Id && g.Deprecated == "0000-00-00 00:00:00")
                        select g).Distinct().ToList();
            }
        }
    }
}
