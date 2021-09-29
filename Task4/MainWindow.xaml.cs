using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Task4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public void UpdateXML(string x)
        {

            XmlDocument document = new XmlDocument();
            document.Load("config.xml");
            XPathNavigator navigator = document.CreateNavigator();
            navigator.MoveToChild("TextSet", "");
            navigator.MoveToChild("Size", "");
            navigator.SetValue(x);
            document.Save("config.xml");

        }

        public void UpdateXML2(string x)
        {

            XmlDocument document = new XmlDocument();
            document.Load("config.xml");
            XPathNavigator navigator = document.CreateNavigator();
            navigator.MoveToChild("TextSet", "");
            navigator.MoveToChild("Color", "");
            navigator.SetValue(x);
            document.Save("config.xml");

        }

        private int fontSize;
        public MainWindow()
        {
            InitializeComponent();

            var reader = XmlReader.Create("config.xml");
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {

                    if (reader.Name == "Size")
                        MyTextBlock.FontSize = reader.ReadElementContentAsInt();
                    if (reader.Name == "Color")
                    {
                        string color = reader.ReadElementContentAsString();
                        switch (color)
                        {
                            case "Red":
                                MyTextBlock.Foreground = Brushes.Red;
                                break;
                            case "Green":
                                MyTextBlock.Foreground = Brushes.Green;
                                break;
                            case "Blue":
                                MyTextBlock.Foreground = Brushes.Blue;
                                break;
                        }
                    }


                }

            }
            reader.Close();
        }
        
        
        //Foreground color buttons
        private void RedButton_Click(object sender, RoutedEventArgs e)
        {
            MyTextBlock.Foreground = Brushes.Red;
            UpdateXML2("Red");
        }

        private void GreenButton_Click(object sender, RoutedEventArgs e)
        {
            MyTextBlock.Foreground = Brushes.Green;
            UpdateXML2("Green");
        }

        private void BlueButton_Click(object sender, RoutedEventArgs e)
        {
            MyTextBlock.Foreground = Brushes.Blue;
            UpdateXML2("Blue");
        }

        //Font size buttons

        private void SmallButton_Click(object sender, RoutedEventArgs e)
        {
            MyTextBlock.FontSize = 11;
            UpdateXML(11.ToString());
        }

        private void MedButton_Click(object sender, RoutedEventArgs e)
        {
            MyTextBlock.FontSize = 22;
            UpdateXML(22.ToString());
        }

        private void LargeButton_Click(object sender, RoutedEventArgs e)
        {
            MyTextBlock.FontSize = 44;
            UpdateXML(44.ToString());
        }

    }
}
