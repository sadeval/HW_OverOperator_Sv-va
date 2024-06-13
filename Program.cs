using System;
using System.Collections.Generic;

namespace StudentManagement
{
    // Класс студента
    class Student
    {
        private string? name; // Имя
        private string? patronymic; // Отчество
        private string? surname; // Фамилия
        private DateTime birthDate; // Дата рождения
        private string? address; // Адрес
        private string? phone; // Номер телефона

        private LinkedList<int> marks = new LinkedList<int>(); // Оценки по зачётам
        private LinkedList<int> courseworks = new LinkedList<int>(); // Оценки по курсовым работам
        private LinkedList<int> exams = new LinkedList<int>(); // Оценки по экзаменам
        private double rating; // Рейтинг

        // Конструктор по умолчанию
        public Student() : this("Unknown", "Unknown", "Unknown", DateTime.MinValue, "Unknown", "Unknown")
        {
            Console.WriteLine("Constructor without parameters");
        }

        // Конструктор с параметрами имени, отчества и фамилии
        public Student(string name, string patronymic, string surname) : this(name, patronymic, surname, DateTime.MinValue, "Unknown", "Unknown")
        {
            Console.WriteLine("Constructor with name, patronymic, surname,");
        }

        // Конструктор с параметрами имени, отчества, фамилии и адреса
        public Student(string name, string patronymic, string surname, string address) : this(name, patronymic, surname, DateTime.MinValue, address, "Unknown")
        {
            Console.WriteLine("Constructor with name, surname, patronymic, address");
        }

        // Основной конструктор
        public Student(string name, string patronymic, string surname, DateTime birthDate, string address, string phone)
        {
            SetName(name);
            SetPatronymic(patronymic);
            SetSurname(surname);
            SetBirthDate(birthDate);
            SetAddress(address);
            SetPhone(phone);
            Console.WriteLine("Main constructor with all parameters");
        }

        // Свойства для доступа к полям
        public string? Name
        {
            get { return name; }
            set { name = value; }
        }

        public string? Patronymic
        {
            get { return patronymic; }
            set { patronymic = value; }
        }

        public string? Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        // Свойство для расчета возраста
        public int Age
        {
            get
            {
                if (birthDate == DateTime.MinValue)
                {
                    return 0;
                }

                int age = DateTime.Now.Year - birthDate.Year;

                // Если день рождения еще не был в текущем году, уменьшаем возраст на 1 год
                if (DateTime.Now < birthDate.AddYears(age))
                {
                    age--;
                }

                return age;
            }
        }

        // Методы для установки значений полей
        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetPatronymic(string patronymic)
        {
            this.patronymic = patronymic;
        }

        public void SetSurname(string surname)
        {
            this.surname = surname;
        }

        public void SetBirthDate(DateTime birthDate)
        {
            this.birthDate = birthDate;
        }

        public void SetAddress(string address)
        {
            this.address = address;
        }

        public void SetPhone(string phone)
        {
            this.phone = phone;
        }

        // Методы для добавления оценок
        public void AddMark(int value)
        {
            if (value < 1 || value > 12) return;
            marks.AddLast(value);
            ResetRating();
        }

        public void AddCoursework(int value)
        {
            if (value < 1 || value > 12) return;
            courseworks.AddLast(value);
            ResetRating();
        }

        public void AddExam(int value)
        {
            if (value < 1 || value > 12) return;
            exams.AddLast(value);
            ResetRating();
        }

        // Метод для редактирования всех оценок
        public void EditMarks(List<int> newMarks, List<int> newCourseworks, List<int> newExams)
        {
            marks = new LinkedList<int>(newMarks);
            courseworks = new LinkedList<int>(newCourseworks);
            exams = new LinkedList<int>(newExams);
            ResetRating();
        }

        // Метод для вывода информации о студенте
        public void PrintStudent()
        {
            Console.WriteLine($"Имя: {name}");
            Console.WriteLine($"Фамилия: {surname}");
            Console.WriteLine($"Отчество: {patronymic}");
            Console.WriteLine($"Дата рождения: {birthDate.ToShortDateString()}");
            Console.WriteLine($"Адрес: {address}");
            Console.WriteLine($"Номер телефона: {phone}");
            Console.Write("Оценки по зачётам: ");
            foreach (var mark in marks)
            {
                Console.Write($"{mark} ");
            }
            Console.WriteLine();
            Console.Write("Оценки по курсовым: ");
            foreach (var coursework in courseworks)
            {
                Console.Write($"{coursework} ");
            }
            Console.WriteLine();
            Console.Write("Оценки по экзаменам: ");
            foreach (var exam in exams)
            {
                Console.Write($"{exam} ");
            }
            Console.WriteLine();
            Console.WriteLine($"Рейтинг оценок: {rating:F1}");
        }

        // Метод для пересчета рейтинга
        private void ResetRating()
        {
            int totalGradesCount = marks.Count + courseworks.Count + exams.Count;

            if (totalGradesCount == 0)
            {
                rating = 0;
                return;
            }

            int totalSum = CalculateSum(marks) + CalculateSum(courseworks) + CalculateSum(exams);
            rating = (double)totalSum / totalGradesCount;
        }

        // Вычисление суммы оценок
        private int CalculateSum(LinkedList<int> list)
        {
            int sum = 0;
            foreach (var item in list)
            {
                sum += item;
            }
            return sum;
        }

        // Метод для получения рейтинга
        public double GetRating()
        {
            return rating;
        }

        // Методы для получения имени и фамилии
        public string? GetName()
        {
            return name;
        }

        public string? GetSurname()
        {
            return surname;
        }

        // Переопределение метода Equals для сравнения студентов
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Student other = (Student)obj;
            return name == other.name &&
                   patronymic == other.patronymic &&
                   surname == other.surname &&
                   birthDate == other.birthDate &&
                   address == other.address &&
                   phone == other.phone;
        }

        // Переопределение метода GetHashCode
        public override int GetHashCode()
        {
            return HashCode.Combine(name, patronymic, surname, birthDate, address, phone);
        }

        // Перегрузка операторов
        public static bool operator ==(Student left, Student right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;
            return left.Equals(right);
        }

        public static bool operator !=(Student left, Student right)
        {
            return !(left == right);
        }

        public static bool operator <(Student left, Student right)
        {
            return left.GetRating() < right.GetRating();
        }

        public static bool operator >(Student left, Student right)
        {
            return left.GetRating() > right.GetRating();
        }

        public static bool operator true(Student s)
        {
            return s.GetRating() > 7;
        }

        public static bool operator false(Student s)
        {
            return s.GetRating() < 7;
        }
    }

    // Класс группы
    class Group
    {
        private List<Student> students; // Список студентов
        private string? groupName; // Название группы
        private string? specialization; // Специализация группы
        private int courseNumber; // Номер курса

        // Конструктор по умолчанию, инициализирующий поля значениями "Unknown"
        public Group()
        {
            students = new List<Student>();
            SetGroupName("Unknown");
            SetSpecialization("Unknown");
            SetCourseNumber(0);
        }

        // Конструктор с параметрами
        public Group(string groupName, string specialization, int courseNumber)
        {
            students = new List<Student>();
            SetGroupName(groupName);
            SetSpecialization(specialization);
            SetCourseNumber(courseNumber);
        }

        // Свойства для доступа к полям
        public string? GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

        public string? Specialization
        {
            get { return specialization; }
            set { specialization = value; }
        }

        public int CourseNumber
        {
            get { return courseNumber; }
            set { courseNumber = value; }
        }

        public int Count
        {
            get { return students.Count; }
        }

        // Методы для установки значений полей
        public void SetGroupName(string groupName)
        {
            this.groupName = groupName;
        }

        public void SetSpecialization(string specialization)
        {
            this.specialization = specialization;
        }

        public void SetCourseNumber(int courseNumber)
        {
            this.courseNumber = courseNumber;
        }

        // Метод для добавления студента в группу
        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        // Метод для редактирования информации о группе
        public void EditGroup(string groupName, string specialization, int courseNumber)
        {
            SetGroupName(groupName);
            SetSpecialization(specialization);
            SetCourseNumber(courseNumber);
        }

        // Метод для перевода студента в другую группу
        public void TransferStudent(Group anotherGroup, Student student)
        {
            if (students.Remove(student))
            {
                anotherGroup.AddStudent(student);
            }
            else
            {
                throw new Exception("Такого студента в группе не существует.");
            }
        }

        // Метод для отчисления самого неуспевающего студента
        public void ExcludeWorstStudent()
        {
            if (students.Count == 0)
            {
                throw new Exception("Нет студента на отчисление.");
            }

            Student worstStudent = students[0];
            foreach (Student student in students)
            {
                if (student.GetRating() < worstStudent.GetRating())
                {
                    worstStudent = student;
                }
            }
            students.Remove(worstStudent);
        }

        // Метод для вывода информации о группе
        public void PrintGroup()
        {
            Console.WriteLine($"Название группы: {groupName}");
            Console.WriteLine($"Специализация группы: {specialization}");
            Console.WriteLine($"Номер курса: {courseNumber}");
            Console.WriteLine("Студенты:");
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {students[i].GetSurname()} {students[i].GetName()}");
            }
        }

        // Индексатор для доступа к студентам по индексу
        public Student this[int index]
        {
            get
            {
                if (index < 0 || index >= students.Count)
                {
                    throw new IndexOutOfRangeException("Индекс за пределами диапазона.");
                }
                return students[index];
            }
        }

        // Переопределение метода Equals для сравнения групп
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Group other = (Group)obj;
            return groupName == other.groupName &&
                   specialization == other.specialization &&
                   courseNumber == other.courseNumber &&
                   students.Count == other.students.Count;
        }

        // Переопределение метода GetHashCode
        public override int GetHashCode()
        {
            return HashCode.Combine(groupName, specialization, courseNumber, students.Count);
        }

        // Перегрузка операторов
        public static bool operator ==(Group left, Group right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;
            return left.Equals(right);
        }

        public static bool operator !=(Group left, Group right)
        {
            return !(left == right);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Создание группы и студентов
                Group group1 = new Group("Группа 1", "Программирование", 1);
                Student student1 = new Student("Василий", "Алибабаевич", "Пупкин", new DateTime(1995, 02, 06), "ул. Литературная, д. 18", "+380955289873");
                Student student2 = new Student("Катя", "Ивановна", "Пушкарёва", new DateTime(1996, 05, 12), "ул. Пушкина, д. 20", "+380955289874");

                // Добавление студентов в группу
                group1.AddStudent(student1);
                group1.AddStudent(student2);

                // Вывод информации о группе
                group1.PrintGroup();
                Console.WriteLine();

                // Создание второй группы и перевод студента
                Group group2 = new Group("Группа 2", "Дизайн", 2);
                group1.TransferStudent(group2, student1);

                // Вывод информации о группах после перевода студента
                Console.WriteLine("После перевода студента:");
                group1.PrintGroup();
                Console.WriteLine();
                group2.PrintGroup();
                Console.WriteLine();

                // Отчисление самого неуспевающего студента
                group1.ExcludeWorstStudent();
                Console.WriteLine("После отчисления самого неуспевающего студента:");
                group1.PrintGroup();
                Console.WriteLine();

                // Редактирование оценок студента
                List<int> newMarks = new List<int> { 12, 10, 11 };
                List<int> newCourseworks = new List<int> { 9, 9, 10 };
                List<int> newExams = new List<int> { 10, 8, 9 };
                student2.EditMarks(newMarks, newCourseworks, newExams);

                Console.WriteLine("После редактирования оценок студента:");
                group1.PrintGroup();
                Console.WriteLine();

                // Проверка студента на допуск
                if (student2)
                {
                    Console.WriteLine("Студент получил допуск");
                }

                // Сравнение групп
                if (group1 == group2)
                {
                    Console.WriteLine("Группы одинаковы");
                }
                else
                {
                    Console.WriteLine("Группы различны");
                }

                // Доступ к студенту по индексу
                Console.WriteLine($"Студент из группы 2: {group2[0].Name} {group2[0].Surname}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}

