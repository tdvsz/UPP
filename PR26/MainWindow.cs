using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private List<string> list = new List<string>();
        private string filePath = "list.txt";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string input = textBox1.Text;

            if (checkBox1.IsChecked == true)
            {
                input = input.ToUpper();
            }

            list.Add(input);
            listBox1.Items.Add(input);

            textBox1.Clear();
        }

        private void listBox1_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                list.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var element in list)
                {
                    writer.WriteLine(element);
                }
            }
        }
    }
}
