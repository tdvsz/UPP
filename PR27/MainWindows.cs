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
using System.Xml.Linq;

namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        private List<Exam> examsList = new List<Exam>();
        private string filePath = "exams.txt";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                examsList.Clear();
                dataGrid.Items.Clear();

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Файл не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        examsList.Add(new Exam(parts[0], parts[1], byte.Parse(parts[2])));
                    }
                }

                dataGrid.ItemsSource = examsList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (Exam exam in examsList)
                {
                    lines.Add($"{exam.Name},{exam.Date},{exam.Mark}");
                }
                File.WriteAllLines(filePath, lines);
                MessageBox.Show("Данные сохранены", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AddMenu_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow();
            if (editWindow.ShowDialog() == true)
            {
                examsList.Add(editWindow.EditedExam);
                dataGrid.Items.Refresh();
            }
        }

        private void EditMenu_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Exam selectedExam = (Exam)dataGrid.SelectedItem;
            EditWindow editWindow = new EditWindow(selectedExam);
            if (editWindow.ShowDialog() == true)
            {
                int index = examsList.IndexOf(selectedExam);
                examsList[index] = editWindow.EditedExam;
                dataGrid.Items.Refresh();
            }
        }

        private void DeleteMenu_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show("Удалить выбранную запись?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                examsList.Remove((Exam)dataGrid.SelectedItem);
                dataGrid.Items.Refresh();
            }
        }

        private void AboutMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ПР25. Меню и панели инструментов\nАвтух М.И.\nП32\nВариант 1",
                "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
