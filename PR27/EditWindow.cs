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
using System.Windows.Shapes;

namespace WpfApp3
{
    public partial class EditWindow : Window
    {
        public Exam EditedExam { get; private set; }

        public EditWindow()
        {
            InitializeComponent();
            EditedExam = new Exam("", "", 0);
        }

        public EditWindow(Exam exam) : this()
        {
            EditedExam = exam;
            txtName.Text = exam.Name;
            txtDate.Text = exam.Date;
            txtMark.Text = exam.Mark.ToString();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!byte.TryParse(txtMark.Text, out byte mark) || mark < 1 || mark > 10)
            {
                MessageBox.Show("ќценка должна быть числом от 1 до 10", "ќшибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            EditedExam = new Exam(txtName.Text, txtDate.Text, mark);
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender == txtDate)
            {
                if (!char.IsDigit(e.Text, 0) && e.Text != ".")
                    e.Handled = true;
            }
            else if (sender == txtMark)
            {
                if (!char.IsDigit(e.Text, 0))
                    e.Handled = true;
            }
        }
    }
}
