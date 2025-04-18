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
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlsAndAPIs
{
    public partial class MainWindow : Window
    {
        private Stack<StrokeCollection> curr = new Stack<StrokeCollection>();
        private Stack<StrokeCollection> temp = new Stack<StrokeCollection>();

        public MainWindow()
        {
            InitializeComponent();
            this.MyInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            this.inkRadio.IsChecked = true;
            this.comboColors.SelectedIndex = 0;

            SaveState();

            MyInkCanvas.Strokes.StrokesChanged += Strokes_StrokesChanged;
        }

        private void Strokes_StrokesChanged(object sender, StrokeCollectionChangedEventArgs e)
        {
            SaveState();
        }

        private void SaveState(bool clearRedo = true)
        {
            if (clearRedo)
            {
                temp.Clear();
                btnRedo.IsEnabled = false;
            }
            curr.Push(new StrokeCollection(MyInkCanvas.Strokes));
        }

        private void Undo()
        {
            if (curr.Count <= 1) return;

            temp.Push(curr.Pop());
            MyInkCanvas.Strokes = new StrokeCollection(curr.Peek());

            btnRedo.IsEnabled = temp.Count > 0;
        }

        private void Redo()
        {
            if (temp.Count == 0) return;

            curr.Push(temp.Pop());
            MyInkCanvas.Strokes = new StrokeCollection(curr.Peek());

            btnRedo.IsEnabled = temp.Count > 0;
        }

        private void RadioButtonClicked(object sender, RoutedEventArgs e)
        {
            switch ((sender as RadioButton)?.Content.ToString())
            { // Эти строки должны совпадать со значениями свойства Content
              // каждого элемента RadioButton.
                case "Ink Mode!":
                    this.MyInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "Erase Mode!":
                    this.MyInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    break;
                case "Select Mode!":
                    this.MyInkCanvas.EditingMode = InkCanvasEditingMode.Select;
                    break;
            }
        }

        private void ColorChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получить выбранный элемент в раскрывающемся списке.
            string colorToUse = (this.comboColors.SelectedItem as StackPanel).Tag.ToString();
            // Изменить цвет, используемый для визуализации штрихов.
            this.MyInkCanvas.DefaultDrawingAttributes.Color =
            (Color)ColorConverter.ConvertFromString(colorToUse);
        }

        private void SaveData(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream("StrokeData.bin", FileMode.Create))
            {
                this.MyInkCanvas.Strokes.Save(fs);
                fs.Close();
            }
        }

        private void LoadData(object sender, RoutedEventArgs e)
        { // Наполнить StrokeCollection из файла.
            using (FileStream fs = new FileStream("StrokeData.bin",
            FileMode.Open, FileAccess.Read))
            {
                StrokeCollection strokes = new StrokeCollection(fs);
                this.MyInkCanvas.Strokes = strokes;
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        { // Очистить все штрихи.
            this.MyInkCanvas.Strokes.Clear();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(sizeBox.Text, out int penSize))
            {
                if (penSize <= 0)
                {
                    penSize = 1;
                    sizeBox.Text = penSize.ToString();
                }

                MyInkCanvas.DefaultDrawingAttributes.Width = penSize;
                MyInkCanvas.DefaultDrawingAttributes.Height = penSize;
            }
            else if (string.IsNullOrEmpty(sizeBox.Text))
            {
                MyInkCanvas.DefaultDrawingAttributes.Width = 1;
                MyInkCanvas.DefaultDrawingAttributes.Height = 1;
            }
        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            Undo();
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            Redo();
        }
    }
}