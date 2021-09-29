using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = new FileInfo(@"C:\Users\georg\source\repos\Tjurins_HW_34\AdditionalTask\bin\Debug\TelephoneBook.xml");
            Console.WriteLine(file.FullName);
            Console.WriteLine(file.CreationTime);
            Console.WriteLine(file.LastAccessTime);
            // etc.

            Console.ReadKey();

        }
    }
}
