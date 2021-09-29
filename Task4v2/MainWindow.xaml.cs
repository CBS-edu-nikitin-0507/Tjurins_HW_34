using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Microsoft.Win32;

namespace Task4v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FontDialog fontDlg = new FontDialog();
        private ColorDialog chooseColorDialog = new ColorDialog();
        public MainWindow()
        {
            InitializeComponent();
            
            //reading from settings file
            ReadFromConf(); 

        }

        private void ReadFromConf()
        {
            NameValueCollection allAppSettings = ConfigurationManager.AppSettings;

            //get background color
            int red = Convert.ToInt32(allAppSettings["BackGroundColor.R"]);
            int green = Convert.ToInt32(allAppSettings["BackGroundColor.G"]);
            int blue = Convert.ToInt32(allAppSettings["BackGroundColor.B"]);

            this.Background = new SolidColorBrush(Color.FromArgb(255, (byte)red, (byte)green, (byte)blue));

            //get font color
            int redf = Convert.ToInt32(allAppSettings["ForegroundColor.R"]);
            int greenf = Convert.ToInt32(allAppSettings["ForegroundColor.G"]);
            int bluef = Convert.ToInt32(allAppSettings["ForegroundColor.B"]);

            MyTextBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, (byte)redf, (byte)greenf, (byte)bluef));

            //get font size
            double fsize = Convert.ToDouble(allAppSettings["FontSize"]);
            MyTextBlock.FontSize = fsize;

            //get font style
            string fstyle = allAppSettings["FontStyle"];
            switch (fstyle)
            {
                case "Normal":
                    MyTextBlock.FontStyle = FontStyles.Normal;
                    break;
                case "Italic":
                    MyTextBlock.FontStyle = FontStyles.Italic;
                    break;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e) //Save btn
        {
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");

            string[] keys = new string[] {"BackGroundColor.R",
                                          "BackGroundColor.G",
                                          "BackGroundColor.B",
                                          "ForegroundColor.R",
                                          "ForegroundColor.G",
                                          "ForegroundColor.B",
                                          "FontSize",
                                          "FontStyle"};

            var bgcolor = ((SolidColorBrush)this.Background).Color;
            var fgcolor = ((SolidColorBrush)MyTextBlock.Foreground).Color;
            string[] values = new string[] { bgcolor.R.ToString(),
                                             bgcolor.G.ToString(),
                                             bgcolor.B.ToString(),
                                             fgcolor.R.ToString(),
                                             fgcolor.G.ToString(),
                                             fgcolor.B.ToString(),
                                             MyTextBlock.FontSize.ToString(),
                                             MyTextBlock.FontStyle.ToString() };

            // Цикл модификации файла конфигурации.
            for (int i = 0; i < keys.Length; i++)
            {
                // Обращаемся к конкретной строке по ключу.
                XmlElement element = node.SelectSingleNode(string.Format("//add[@key='{0}']", keys[i])) as XmlElement;

                // Если строка с таким ключем существует - записываем значение.
                if (element != null) { element.SetAttribute("value", values[i]); }
                else
                {
                    // Иначе: создаем строку и формируем в ней пару [ключ]-[значение].
                    element = doc.CreateElement("add");
                    element.SetAttribute("key", keys[i]);
                    element.SetAttribute("value", values[i]);
                    node.AppendChild(element);
                }
            }
            doc.Save(Assembly.GetExecutingAssembly().Location + ".config");

            RegistryKey key = Registry.CurrentUser;
            RegistryKey subKey = key.OpenSubKey("Software", true);
            RegistryKey subSubKey = subKey.CreateSubKey("Task4v2Settings");

            subSubKey.SetValue("BGcolorR", bgcolor.R.ToString());
            subSubKey.SetValue("BGcolorG", bgcolor.G.ToString());
            subSubKey.SetValue("BGcolorB", bgcolor.B.ToString());

            subSubKey.SetValue("FcolorR", fgcolor.R.ToString());
            subSubKey.SetValue("FcolorG", fgcolor.G.ToString());
            subSubKey.SetValue("FcolorB", fgcolor.B.ToString());

            subSubKey.SetValue("FSize", MyTextBlock.FontSize.ToString());
            subSubKey.SetValue("FStyle", MyTextBlock.FontStyle.ToString());

            subKey.Close();
            subSubKey.Close();

        }
        private XmlDocument loadConfigDocument()
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(Assembly.GetExecutingAssembly().Location + ".config");
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) //BG color
        {
            if (chooseColorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.Background = new SolidColorBrush(Color.FromArgb(chooseColorDialog.Color.A, chooseColorDialog.Color.R, chooseColorDialog.Color.G, chooseColorDialog.Color.B));
        }
        private void Button_Click_2(object sender, RoutedEventArgs e) //Text color
        {
            if (chooseColorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                MyTextBlock.Foreground = new SolidColorBrush(Color.FromArgb(chooseColorDialog.Color.A, chooseColorDialog.Color.R, chooseColorDialog.Color.G, chooseColorDialog.Color.B));
        }
        private void Button_Click_3(object sender, RoutedEventArgs e) //Font style and size
        {
            if (fontDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MyTextBlock.FontStyle = fontDlg.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
                MyTextBlock.FontSize = fontDlg.Font.Size;
            }

        }
        private void Button_Click_4(object sender, RoutedEventArgs e) //Load from REG
        {
            RegistryKey key = Registry.CurrentUser;
            RegistryKey subKey = key.OpenSubKey(@"SOFTWARE\Task4v2Settings");

            int red = Convert.ToInt32(subKey.GetValue("BGcolorR"));
            int green = Convert.ToInt32(subKey.GetValue("BGcolorG"));
            int blue = Convert.ToInt32(subKey.GetValue("BGcolorB"));
            this.Background = new SolidColorBrush(Color.FromArgb(255, (byte)red, (byte)green, (byte)blue));

            int redf = Convert.ToInt32(subKey.GetValue("FcolorR"));
            int greenf = Convert.ToInt32(subKey.GetValue("FcolorG"));
            int bluef = Convert.ToInt32(subKey.GetValue("FcolorB"));
            MyTextBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, (byte)redf, (byte)greenf, (byte)bluef));

            MyTextBlock.FontSize = Convert.ToDouble(subKey.GetValue("FSize"));
            string style = subKey.GetValue("FStyle") as string;
            switch (style)
            {
                case "Normal":
                    MyTextBlock.FontStyle = FontStyles.Normal;
                    break;
                case "Italic":
                    MyTextBlock.FontStyle = FontStyles.Italic;
                    break;
            }
            subKey.Close();
        }
    }
}
