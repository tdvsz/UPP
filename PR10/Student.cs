using System;
using System.Collections;

namespace upp
{
    public class StudentCollection : IList<Student>, IEnumerator<Student>, IComparer<Student>
    {
        private List<Student> students = new List<Student>();
        private int currentIndex = -1;

        // Ilist
        public Student this[int index] 
        {
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }

        public int Count => students.Count;

        public bool IsReadOnly => false;

        public void Add(Student item)
        {
            students.Add(item);
        }

        public void Clear()
        {
            students.Clear();
        }

        public bool Contains(Student item)
        {
            return students.Contains(item);
        }

        public void CopyTo(Student[] array, int arrayIndex)
        {
            students.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Student> GetEnumerator()
        {
            return this;
        }

        public int IndexOf(Student item)
        {
            return students.IndexOf(item);
        }

        public void Insert(int index, Student item)
        {
            students.Insert(index, item);
        }

        public bool Remove(Student item)
        {
            return students.Remove(item);
        }

        public void RemoveAt(int index)
        {
            students.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // IEnumerator
        public Student Current
        {
            get
            {
                if (currentIndex == -1 || currentIndex >= students.Count)
                {
                    throw new InvalidOperationException();
                }
                return students[currentIndex];
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            Reset();
        }

        public bool MoveNext()
        {
            if (currentIndex < students.Count - 1)
            {
                currentIndex++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            currentIndex = -1;
        }

        // IComparer
        private SortField sortField;
        private SortDirection sortDirection;

        public int Compare(Student x, Student y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1 * GetDirectionMultiplier();
            if (y == null) return 1 * GetDirectionMultiplier();

            int result = 0;

            switch (sortField)
            {
                case SortField.Surname:
                    result = string.Compare(x.Surname, y.Surname, StringComparison.Ordinal);
                    break;
                case SortField.Specialty:
                    result = string.Compare(x.Specialty, y.Specialty, StringComparison.Ordinal);
                    break;
                case SortField.Course:
                    result = x.Course.CompareTo(y.Course);
                    break;
            }

            return result * GetDirectionMultiplier();
        }

        private int GetDirectionMultiplier()
        {
            return sortDirection == SortDirection.Ascending ? 1 : -1;
        }

        public void Sort(SortField field, SortDirection direction)
        {
            sortField = field;
            sortDirection = direction;
            students.Sort(this);
        }
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }

    public enum SortField
    {
        Surname,
        Course,
        Specialty
    }

    public class Student
    {
        public string Surname;
        public string Name;
        public string Patronymic;
        public string Specialty;
        public byte Course;
        public DateTime Birthday;

        public Student(string surname, string name, string patronymic, string specialty, byte course, DateTime birthday)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Specialty = specialty;
            Course = course;
            Birthday = birthday;
        }

        public void ValidateNamePart(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Поле не может быть пустым");
            }

            if (value.Length > 15)
            {
                throw new Exception("Поле не может может быть больше 15 символов");
            }
        }

        //public void ValidateSpecialty(string specialty)
        //{
        //    if (specialty.Length != 2)
        //    {
        //        throw new Exception("Специальность должна состоять из 2 букв");
        //    }
        //}

        //public void ValidateCourse(byte course)
        //{
        //    if (course < 1 || course > 5)
        //    {
        //        throw new Exception("Курс должен быть числом от 1 до 5");
        //    }
        //}

        public void ValidateData(string surname, string name, string patronymic, string specialty, byte course, out bool q)
        {
            ValidateNamePart(surname);
            ValidateNamePart(name);
            ValidateNamePart(patronymic);

            if (specialty.Length != 2)
            {
                throw new Exception("Специальность должна состоять из 2 букв");
            }

            if (course < 1 || course > 5)
            {
                throw new Exception("Курс должен быть числом от 1 до 5");
            }

            q = true;
        }

        public override string ToString()
        {
            return $"{Surname} {Name} {Patronymic}. Специальность: {Specialty} {Course} курс. Дата рождения: {Birthday:dd.MM.yyyy}";
        }
    }
}