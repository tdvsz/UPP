using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class MainForm : Form
    {
        List<Exam> examsList = new List<Exam>();
        public string filePath = "exams.txt";

        public MainForm()
        {
            InitializeComponent();
        }

        private void openMenuStrip_Click(object sender, EventArgs e)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    string line = reader.ReadString();
                    string[] parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        string name = parts[0];
                        string date = parts[1];
                        byte mark = byte.Parse(parts[2]);
                        examsList.Add(new Exam(name, date, mark));
                    }
                }
            }

            dataGridView1.Rows.Clear();
            foreach (Exam exam in examsList)
            {
                dataGridView1.Rows.Add(exam.Name, exam.Date, exam.Mark);
            }
        }

        private void saveMenuStrip_Click(object sender, EventArgs e)
        {
            examsList.Clear();
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string name = row.Cells[0].Value.ToString();
                        string date = row.Cells[1].Value.ToString();
                        byte mark = byte.Parse(row.Cells[2].Value.ToString());

                        examsList.Add(new Exam(name, date, mark));
                    }
                }

                foreach (var exam in examsList)
                {
                    string line = $"{exam.Name},{exam.Date},{exam.Mark}";
                    writer.Write(line);
                }
            }
        }

        private void exitMenuStrip_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void addMenuStrip_Click(object sender, EventArgs e)
        {
            EditForm editForm = new EditForm();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Rows.Add(editForm.EditedExam.Name,
                editForm.EditedExam.Date,
                editForm.EditedExam.Mark);
            }
        }

        private void editMenuStrip_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для редактирования!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            Exam examToEdit = new Exam(
                selectedRow.Cells[0].Value.ToString(),
                selectedRow.Cells[1].Value.ToString(),
                Convert.ToByte(selectedRow.Cells[2].Value));

            EditForm editForm = new EditForm(examToEdit);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                selectedRow.Cells[0].Value = editForm.EditedExam.Name;
                selectedRow.Cells[1].Value = editForm.EditedExam.Date;
                selectedRow.Cells[2].Value = editForm.EditedExam.Mark;
            }
        }

        private void deleteMenuStrip_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для удаления!", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Удалить выбранную запись?", "Подтверждение",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ПР25. Меню и панели инструментов\n" +
                "Автух М.И.\n" +
                "П32\n" +
                "Вариант 1\n",
                           "О программе",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information);
        }
    }
}
