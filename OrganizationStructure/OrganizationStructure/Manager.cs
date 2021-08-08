using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure
{
    public class Manager : Worker
    {
        public override int SetSalary(Department dept)
        {
            int result = 0;
            int sum = 0;

            // Суммируем зарплату всех сотрудников кроме руководителя
            if (dept.Workers != null)
            {
                foreach (var w in dept.Workers)
                {
                    if (w.Category == "Начальник отдела") continue;
                    sum += w.Salary;
                }      
            }

            // Если есть подчиненные департаменты, добавляем суммы зарплат их сотрудников
            if (dept.Departments != null)
            {
                foreach (var d in dept.Departments)
                {
                    foreach (var w in d.Workers)
                    {
                        if (w.Category != "Начальник отдела") continue;
                        sum += w.SetSalary(d);
                    }
                }
            }

            result = sum;
            sum = (int)(sum * 0.15f);
            if (sum < 1300) sum = 1300;
            this.Salary = sum;

            // Добавляем к результату зарплату менеджера
            result += this.Salary;

            return result;
        }
    }
}
