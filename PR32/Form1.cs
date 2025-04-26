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
                    "1. Подключиться к БД\n" +
                    "2. Добавить студента в БД\n" +
                    "3. Просмотреть всех студентов\n" +
                    "4. Отключиться от БД");
                Console.Write("Действие: ");
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
                Console.WriteLine("Отключено от БД.");
            }
            else
            {
                Console.WriteLine("Соединение не установлено.");
            }
            Console.ReadKey();
        }

        private void ShowStudents()
        {
            Console.Clear();
            if (dataBase == null)
            {
                Console.WriteLine("Сначала подключитесь к БД.");
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
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.ReadKey();
        }

        private void AddStudent()
        {
            Console.Clear();
            if (dataBase == null)
            {
                Console.WriteLine("Сначала подключитесь к БД");
                return;
            }

            string lastName, firstName, patronymic, specialty;
            byte course;
            DateTime birthdayDate;

            while (true)
            {
                Console.WriteLine("Фамилия студента (не более 15 символов):");
                lastName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(lastName) && lastName.Length <= 15) break;
                Console.WriteLine("Ошибка: Фамилия не должна превышать 15 символов.");
            }

            while (true)
            {
                Console.WriteLine("Имя студента (не более 15 символов):");
                firstName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(firstName) && firstName.Length <= 15) break;
                Console.WriteLine("Ошибка: Имя не должно превышать 15 символов.");
            }

            while (true)
            {
                Console.WriteLine("Отчество студента (не более 15 символов):");
                patronymic = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(patronymic) && patronymic.Length <= 15) break;
                Console.WriteLine("Ошибка: Отчество не должно превышать 15 символов.");
            }

            while (true)
            {
                Console.WriteLine("Специальность (ровно 2 символа):");
                specialty = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(specialty) && specialty.Length == 2) break;
                Console.WriteLine("Ошибка: Специальность должна состоять из 2 символов.");
            }

            while (true)
            {
                Console.WriteLine("Курс (от 1 до 5):");
                if (byte.TryParse(Console.ReadLine(), out course) && course >= 1 && course <= 5) break;
                Console.WriteLine("Ошибка: Курс должен быть числом от 1 до 5.");
            }

            while (true)
            {
                Console.WriteLine("Дата рождения (дд.мм.гггг):");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out birthdayDate)) break;
                Console.WriteLine("Ошибка: Неверный формат даты. Используйте формат: дд.мм.гггг");
            }

            string birthday = birthdayDate.ToString("dd.MM.yyyy");

            string query = $"INSERT INTO Student (lastname, firstname, patronymic, specialty, course, birthday) " +
                $"VALUES ('{lastName}', '{firstName}', '{patronymic}', '{specialty}', {course}, '{birthday}')";
            try
            {
                dataBase.Insert(query);
                Console.WriteLine("Студент добавлен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении: {ex.Message}");
            }

            Console.ReadKey();
        }

        private void ConnectToDB()
        {
            Console.Clear();
            Console.WriteLine(
                "Подключение к БД\n" +
                "1. SqlConnection\n" +
                "2. OleDbConnection\n" +
                "3. OdbcConnection\n" +
                "4. Назад");
            Console.Write("Действие: ");
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
                    Console.WriteLine("Неверный выбор типа подключения.");
                    return;
            }

            Console.Clear();

            try
            {
                dataBase = new MyDataBase(connectionString, connectionType);
                dataBase.Connect();
                Console.WriteLine("Подключение успешно установлено!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подключения: {ex.Message}");
            }
            Console.ReadKey();
        }
    }
}
