using System;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        int id;

        public Form1()
        {
            InitializeComponent();
            LoadData();
            dataGridView1.Columns["ID"].Visible = false;
            editBtn.Enabled = false;
            delBtn.Enabled = false;
            cancelBtn.Visible = false;
        }

        private void LoadData()
        {
            using (var context = new AppDbContext())
            {
                dataGridView1.DataSource = context.StudentUpp.ToList();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            string lastname = textBox1.Text.Trim();
            string firstname = textBox2.Text.Trim();
            string patronymic = textBox3.Text.Trim();
            string specialty = textBox4.Text.Trim();
            string courseText = textBox5.Text.Trim();
            DateTime birthday = dateTimePicker1.Value;

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

            using (var context = new AppDbContext())
            {
                var student = new StudentUpp
                {
                    lastname = lastname,
                    firstname = firstname,
                    patronymic = patronymic,
                    specialty = specialty,
                    course = course,
                    birthday = birthday
                };

                context.StudentUpp.Add(student);
                context.SaveChanges();
                MessageBox.Show("Студент добавлен.");

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                dateTimePicker1.Text = "";

                LoadData();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                delBtn.Enabled = true;

                id = Convert.ToInt32(row.Cells["ID"].Value);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            addBtn.Enabled = false;
            delBtn.Enabled = false;
            dataGridView1.Enabled = false;

            editBtn.Enabled = true;
            cancelBtn.Visible = true;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["lastname"].Value.ToString();
                textBox2.Text = row.Cells["firstname"].Value.ToString();
                textBox3.Text = row.Cells["patronymic"].Value.ToString();
                textBox4.Text = row.Cells["specialty"].Value.ToString();
                textBox5.Text = row.Cells["course"].Value.ToString();
                dateTimePicker1.Text = row.Cells["birthday"].Value.ToString();

                id = Convert.ToInt32(row.Cells["ID"].Value);
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            addBtn.Enabled = true;
            editBtn.Enabled = false;
            dataGridView1.Enabled = true;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            dateTimePicker1.Text = "";
            cancelBtn.Visible = false;
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            string lastname = textBox1.Text.Trim();
            string firstname = textBox2.Text.Trim();
            string patronymic = textBox3.Text.Trim();
            string specialty = textBox4.Text.Trim();
            string courseText = textBox5.Text.Trim();
            DateTime birthday = dateTimePicker1.Value;

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

            using (var context = new AppDbContext())
            {
                var student = context.StudentUpp.Find(id);
                if (student != null)
                {

                    student.lastname = lastname;
                    student.firstname = firstname;
                    student.patronymic = patronymic;
                    student.specialty = specialty;
                    student.course = course;
                    student.birthday = birthday;
                };
                context.SaveChanges();
                MessageBox.Show("Изменено.");
                addBtn.Enabled = true;
                editBtn.Enabled = false;
                dataGridView1.Enabled = true;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                dateTimePicker1.Text = "";
                cancelBtn.Visible = false;
                LoadData();
            }
        }
        private void delBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                using (var context = new AppDbContext())
                {
                    var student = context.StudentUpp.Find(id);
                    if (student != null)
                    {
                        context.StudentUpp.Remove(student);
                        context.SaveChanges();
                        MessageBox.Show("Удалено.");
                        LoadData();
                    }
                }
            }
        }
    }
}
