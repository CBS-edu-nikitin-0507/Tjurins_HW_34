using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            //var document = new XPathDocument("TelephoneBook.xml");
            //XPathNavigator navigate = document.CreateNavigator();

            var reader = XmlReader.Create(@"C:\Users\georg\source\repos\Tjurins_HW_34\AdditionalTask\bin\Debug\TelephoneBook.xml");
            while (reader.Read())
            {
                if(reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name.Equals("Contact"))
                        Console.WriteLine("Phone number: {0}", reader.GetAttribute("TelephoneNumber"));
                }
            }
            Console.ReadKey();
        }
    }
}
