using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OrganizationStructure
{
    //public class Department : Hierarchy


    public class Department
    {
        // Название департамента
        public string Name { get; set; }

        // Список вложенных департаментов
        public List<Department> Departments { get; set; }

        // Список сотрудников
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
        //public override object Append(object _d, int current_depth, bool director)
        public Department Append(string _d, int current_depth, bool director)
        {
            Department d = new Department();
            //d.Name = _d as string;
            d.Name = _d;

            if (!director)
            {
                // Рекурсивно создаем департаменты нижнего уровня
                if (current_depth > 0)
                {
                    current_depth--;
                    d.Departments = new List<Department>();
                    for (int i = 1; i < rand.Next(2, 5); i++)
                    {
                        //Department vd = Append(d.Name + i.ToString(), current_depth, false) as Department;
                        Department vd = Append(d.Name + i.ToString(), current_depth, false);
                        d.Departments.Add(vd);
                    }
                }
                // Добавляем сотрудников
                CreateWorkers(d);
                // Начисляем зарплату руководителю
                ManagerSalary(d);
            }
            else
            {
                d.Departments = new List<Department>();
                // Создаем подразделения
                for (int i = 1; i < 21; i++)
                {                    
                    Department vd = Append("Отдел_" + i.ToString(), rand.Next(1, 4), false) as Department;
                    d.Departments.Add(vd);
                }

                // Добавляем директора
                Worker w = new Worker();
                w.FirstName = "Имя_директора";
                w.LastName = "Фамилия_директора";
                w.Age = rand.Next(40, 65);
                w.Category = "Директор организации";
                w.Salary = 0; // Зарплату расчитаем потом
                d.Workers = new List<Worker>();
                d.Workers.Add(w);

                // Начисляем зарплату директору
                ManagerSalary(d);
            }
            return d;
        }



        /// <summary>
        /// Метод добавления сотрудников в департамент
        /// </summary>
        /// <param name="dept"></param>
        private void CreateWorkers(Department dept)
        {
            dept.Workers = new List<Worker>();
            // Добавляем руководителя
            Worker w = new Worker();
            w.FirstName = "Имя_руководителя";
            w.LastName = "Фамилия_руководителя";
            w.Age = rand.Next(20, 65);
            w.Category = "Начальник отдела";
            w.Salary = 0; // Зарплату расчитаем потом
            dept.Workers.Add(w);
            // Теперь добавляем сотрудников
            for (int i = 1; i < rand.Next(5, 21); i++)
            {
                Worker vw = new Worker();
                vw.FirstName = "Имя_" + i.ToString();
                vw.LastName = "Фамилия_" + i.ToString();
                vw.Age = rand.Next(20, 65);
                int category = rand.Next(2);
                if (category == 0)
                {
                    vw.Category = "Кодер";
                    vw.Salary = 12 * 8 * 22;
                }
                else
                {
                    vw.Category = "Интерн";
                    vw.Salary = 500;
                }
                dept.Workers.Add(vw);
            }
        }


        /// <summary>
        /// Метод вычисления зарплаты руководителя
        /// </summary>
        /// <param name="dept">Департамент</param>
        /// <returns>Сумма зарплат всех сотрудников департамента</returns>
        int ManagerSalary(Department dept)
        {
            int result = 0;
            int sum = 0;
            foreach (var w in dept.Workers)
            {
                if (w.Category == "Начальник отдела") continue;
                sum += w.Salary;
            }
            if (dept.Departments == null)
            {
                sum = (int)(sum * 0.15f);
                if (sum < 1300) sum = 1300;
                foreach (var w in dept.Workers)
                {
                    if (w.Category == "Начальник отдела") w.Salary = sum;
                    result += w.Salary;
                }
            }
            else
            {
                foreach (var d in dept.Departments)
                {
                    sum += ManagerSalary(d);
                }
                result = sum;
                sum = (int)(sum * 0.15f);
                if (sum < 1300) sum = 1300;
                foreach (var w in dept.Workers)
                {
                    if (w.Category == "Начальник отдела" || w.Category == "Директор организации")
                    {
                        w.Salary = sum;
                        result += w.Salary;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
