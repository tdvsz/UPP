using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class EditForm : Form
    {
        public Exam EditedExam { get; private set; }

        public EditForm()
        {
            InitializeComponent();
            textBox1.MaxLength = 50;

            textBox2.MaxLength = 10;
            textBox2.KeyPress += DateTextBox_KeyPress;

            textBox3.MaxLength = 2;
            textBox3.KeyPress += MarkTextBox_KeyPress;
        }

        public EditForm(Exam exam) : this()
        {
            EditedExam = exam;
            textBox1.Text = exam.Name;
            textBox2.Text = exam.Date;
            textBox3.Text = exam.Mark.ToString();
        }

        private void DateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void MarkTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditedExam = new Exam(textBox1.Text, textBox2.Text, byte.Parse(textBox3.Text));
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
