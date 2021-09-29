using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AdditionalTask
{
    class Program
    {
        public static XmlWriterSettings SetXMLSettings()
        {
            var xmlSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
            };
            return xmlSettings;
        }
        public static  void FillupPhonebook(XmlWriter xmlWriter, string name, string phoneNum)
        {
            xmlWriter.WriteStartElement("Contact");
            xmlWriter.WriteStartAttribute("TelephoneNumber");
            xmlWriter.WriteString(phoneNum);
            xmlWriter.WriteEndAttribute();
            xmlWriter.WriteString(name);
            xmlWriter.WriteEndElement();
        }
        static void Main(string[] args)
        {
            string name, phone;
            bool flag = true;

            XmlWriter xmlWriter = XmlWriter.Create("TelephoneBook.xml", SetXMLSettings());
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("MyContacts");
            while (flag)
            {
                Console.Write("\nName: "); name = Console.ReadLine();
                Console.Write("Phone number: "); phone = Console.ReadLine();
                FillupPhonebook(xmlWriter, name, phone);

                Console.WriteLine("\nQ: Want to add one more contact ?");
                Console.Write("A: ");
                if (Console.ReadLine().ToLower().Equals("no"))
                    flag = false;
            }
            xmlWriter.Close();


        }

    }
}
