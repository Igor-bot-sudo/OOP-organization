using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace OrganizationStructure
{
    public class Department
    {
        // Название департамента
        public string Name { get; set; }

        // Список вложенных департаментов
        public List<Department> Departments { get; set; }

        // Список сотрудников департамента
        public List<Worker> Workers { get; set; }

        Random rand = new Random();


        public static void CreateOrganization()
        {
            string json;
            Department dept = new Department();
            dept.Name = "Компания Лучшие кодеры";
            // Поток записи данных в JSON-файл
            using (StreamWriter sw = new StreamWriter("Organization.json", false, Encoding.UTF8))
            {
                json = JsonConvert.SerializeObject(dept.Append(dept.Name, 1, true));
                sw.WriteLine(json);
            }
        }


        /// <summary>
        /// Метод создания департамента
        /// </summary>
        /// <param name="_d">Название департамента</param>
        /// <param name="current_depth">Текущий уровень вложенности</param>
        /// <param name="director">Логический параметр, определяющий создается отдельный департамент или вся организация</param>
        /// <returns>Созданный департамент</returns>
        public Department Append(string deptName, int current_depth, bool director)
        {
            Department d = new Department();
            d.Name = deptName;
            d.Workers = new List<Worker>();

            if (!director)
            {
                // Рекурсивно создаем департаменты нижнего уровня
                if (current_depth > 0)
                {
                    current_depth--;
                    d.Departments = new List<Department>();
                    for (int i = 1; i < rand.Next(2, 5); i++)
                    {
                        Department vd = Append(d.Name + i.ToString(), current_depth, false);
                        d.Departments.Add(vd);
                    }
                }
                // Добавляем сотрудников
                CreateWorkers(d);
            }
            else
            {
                d.Departments = new List<Department>();
                // Создаем подразделения
                for (int i = 1; i < 21; i++)
                {
                    Department vd = Append("Отдел_" + i.ToString(), rand.Next(1, 4), false);
                    d.Departments.Add(vd);
                }

                // Добавляем директора
                Worker chief = new Chief();
                chief.FirstName = "Имя_директора";
                chief.LastName = "Фамилия_директора";
                chief.Age = rand.Next(40, 65);
                chief.Category = "Директор организации";
                chief.SetSalary(d);
                d.Workers.Add(chief);
            }
            return d;
        }


        /// <summary>
        /// Метод добавления сотрудников в департамент
        /// </summary>
        /// <param name="dept">Департамент</param>
        private void CreateWorkers(Department dept)
        {
            // Добавляем сотрудников
            for (int i = 1; i < rand.Next(5, 21); i++)
            {
                if (rand.Next(2) == 0)
                {
                    Worker coder = new Coder();
                    coder.FirstName = "Имя_" + i.ToString();
                    coder.LastName = "Фамилия_" + i.ToString();
                    coder.Age = rand.Next(23, 45);
                    coder.Category = "Кодер";
                    coder.SetSalary(dept);
                    dept.Workers.Add(coder);
                }
                else
                {
                    Worker intern = new Intern();
                    intern.FirstName = "Имя_" + i.ToString();
                    intern.LastName = "Фамилия_" + i.ToString();
                    intern.Age = rand.Next(20, 35);
                    intern.Category = "Интерн";
                    intern.SetSalary(dept);
                    dept.Workers.Add(intern);
                }
            }

            Worker manager = new Manager();
            manager.FirstName = "Имя_руководителя";
            manager.LastName = "Фамилия_руководителя";
            manager.Age = rand.Next(30, 45);
            manager.Category = "Начальник отдела";
            // Для исключения повторных действий зарплата руководителей департаментов
            // начисляется при рекурсивном вычислении зарплаты директора организации
            manager.Salary = 0;
            dept.Workers.Add(manager);
        }
    }
}
