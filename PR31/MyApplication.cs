using System;
using System.Collections.Generic;
using System.Data;

namespace ConsoleApp5
{
    class MyApplication
    {
        private MyDataBase dataBase;

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(
                    "1. ������������ � ��\n" +
                    "2. �������� �������� � ��\n" +
                    "3. ����������� ���� ���������\n" +
                    "4. ����������� �� ��");
                Console.Write("��������: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ConnectToDB();
                        break;
                    case "2":
                        AddStudent();
                        break;
                    case "3":
                        ShowStudents();
                        break;
                    case "4":
                        DisconnectDB();
                        break;
                    case "":
                        return;
                }
            }
        }

        private void DisconnectDB()
        {
            Console.Clear();
            if (dataBase != null)
            {
                dataBase.Disconnect();
                dataBase = null;
                Console.WriteLine("��������� �� ��.");
            }
            else
            {
                Console.WriteLine("���������� �� �����������.");
            }
            Console.ReadKey();
        }

        private void ShowStudents()
        {
            Console.Clear();
            if (dataBase == null)
            {
                Console.WriteLine("������� ������������ � ��.");
                Console.ReadKey();
                return;
            }

            string query = "SELECT * FROM Student";
            try
            {
                DataTable table = dataBase.Select(query);
                foreach (DataRow row in table.Rows)
                {
                    string birthday = row["birthday"].ToString();

                    if (DateTime.TryParse(row["birthday"].ToString(), out DateTime formattedDate))
                    {
                        birthday = formattedDate.ToString("dd.MM.yyyy");
                    }

                    Console.WriteLine($"{row["lastname"]} {row["firstname"]} {row["patronymic"]} {row["specialty"]} {row["course"]} {birthday}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"������: {ex.Message}");
            }
            Console.ReadKey();
        }

        private void AddStudent()
        {
            Console.Clear();
            if (dataBase == null)
            {
                Console.WriteLine("������� ������������ � ��");
                return;
            }

            string lastName, firstName, patronymic, specialty;
            byte course;
            DateTime birthdayDate;

            while (true)
            {
                Console.WriteLine("������� �������� (�� ����� 15 ��������):");
                lastName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(lastName) && lastName.Length <= 15) break;
                Console.WriteLine("������: ������� �� ������ ��������� 15 ��������.");
            }

            while (true)
            {
                Console.WriteLine("��� �������� (�� ����� 15 ��������):");
                firstName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(firstName) && firstName.Length <= 15) break;
                Console.WriteLine("������: ��� �� ������ ��������� 15 ��������.");
            }

            while (true)
            {
                Console.WriteLine("�������� �������� (�� ����� 15 ��������):");
                patronymic = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(patronymic) && patronymic.Length <= 15) break;
                Console.WriteLine("������: �������� �� ������ ��������� 15 ��������.");
            }

            while (true)
            {
                Console.WriteLine("������������� (����� 2 �������):");
                specialty = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(specialty) && specialty.Length == 2) break;
                Console.WriteLine("������: ������������� ������ �������� �� 2 ��������.");
            }

            while (true)
            {
                Console.WriteLine("���� (�� 1 �� 5):");
                if (byte.TryParse(Console.ReadLine(), out course) && course >= 1 && course <= 5) break;
                Console.WriteLine("������: ���� ������ ���� ������ �� 1 �� 5.");
            }

            while (true)
            {
                Console.WriteLine("���� �������� (��.��.����):");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out birthdayDate)) break;
                Console.WriteLine("������: �������� ������ ����. ����������� ������: ��.��.����");
            }

            string birthday = birthdayDate.ToString("dd.MM.yyyy");

            string query = $"INSERT INTO Student (lastname, firstname, patronymic, specialty, course, birthday) " +
                $"VALUES ('{lastName}', '{firstName}', '{patronymic}', '{specialty}', {course}, '{birthday}')";
            try
            {
                dataBase.Insert(query);
                Console.WriteLine("������� ��������.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"������ ��� ����������: {ex.Message}");
            }

            Console.ReadKey();
        }

        private void ConnectToDB()
        {
            Console.Clear();
            Console.WriteLine(
                "����������� � ��\n" +
                "1. SqlConnection\n" +
                "2. OleDbConnection\n" +
                "3. OdbcConnection\n" +
                "4. �����");
            Console.Write("��������: ");
            string choice = Console.ReadLine();

            ConnectionType connectionType;
            string connectionString;

            switch (choice)
            {
                case "1":
                    connectionType = ConnectionType.Sql;
                    connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB; AttachDbFilename=Students.accdb; Integrated Security = True";
                    break;
                case "2":
                    connectionType = ConnectionType.OleDb;
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Students.accdb;Persist Security Info=False;";
                    break;
                case "3":
                    connectionType = ConnectionType.Odbc;
                    connectionString = "Driver={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=Students.accdb;";
                    break;
                case "4":
                    Console.Clear();
                    Start();
                    return;
                default:
                    Console.WriteLine("�������� ����� ���� �����������.");
                    return;
            }

            Console.Clear();

            try
            {
                dataBase = new MyDataBase(connectionString, connectionType);
                dataBase.Connect();
                Console.WriteLine("����������� ������� �����������!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"������ �����������: {ex.Message}");
            }
            Console.ReadKey();
        }
    }
}
