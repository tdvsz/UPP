using System.Reflection;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        dynamic classLib;
        public Form1()
        {
            InitializeComponent();
            LoadDll();
        }

        private void LoadDll()
        {
            try
            {
                string path = "ClassLibrary2.dll";
                var asm = Assembly.LoadFrom(path);
                var type = asm.GetType("ClassLibrary2.Class1");
                classLib = Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double x = double.Parse(textBox1.Text);

            label3.Text = Convert.ToString(classLib.CalculateY(x));
        }
    }
}