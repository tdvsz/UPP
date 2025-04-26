using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            saveBtn.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            studentTableAdapter.Fill(studentsDataSet.Student);
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            studentTableAdapter.Update(studentsDataSet.Student);
            studentTableAdapter.Fill(studentsDataSet.Student);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                studentBindingSource.EndEdit();

                string lastname = lastnameTB.Text;
                string firstname = firstnameTB.Text;
                string patronymic = patronymicTB.Text;
                string specialty = specialtyTB.Text;
                string courseText = courseTB.Text;
                DateTime birthdayT = dateTimePicker1.Value;

                if (lastname.Length > 15 || firstname.Length > 15 || patronymic.Length > 15)
                {
                    MessageBox.Show("Фамилия, имя и отчество не должны превышать 15 символов.");
                    return;
                }

                if (specialty.Length != 2)
                {
                    MessageBox.Show("Код специальности должен содержать ровно 2 символа.");
                    return;
                }

                if (!int.TryParse(courseText, out int course) || course < 1 || course > 5)
                {
                    MessageBox.Show("Курс должен быть целым числом от 1 до 5.");
                    return;
                }

                string birthday = birthdayT.ToString("yyyy-MM-dd");

                studentTableAdapter.Update(studentsDataSet.Student);

                MessageBox.Show("Запись добавлена!");

                dataGridView1.Enabled = true;

                bindingNavigatorAddNewItem.Enabled = true;
                bindingNavigatorDeleteItem.Enabled = true;
                bindingNavigatorMoveFirstItem.Enabled = true;
                bindingNavigatorMoveLastItem.Enabled = true;
                bindingNavigatorMoveNextItem.Enabled = true;
                bindingNavigatorMovePreviousItem.Enabled = true;

                saveBtn.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Text = "";
            dataGridView1.Enabled = false;

            bindingNavigatorAddNewItem.Enabled = false;
            bindingNavigatorDeleteItem.Enabled = false;
            bindingNavigatorMoveFirstItem.Enabled = false;
            bindingNavigatorMoveLastItem.Enabled = false;
            bindingNavigatorMoveNextItem.Enabled = false;
            bindingNavigatorMovePreviousItem.Enabled = false;

            saveBtn.Visible = true;
        }
    }
}