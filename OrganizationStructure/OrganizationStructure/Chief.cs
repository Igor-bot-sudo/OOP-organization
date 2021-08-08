using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure
{
    public class Chief : Worker
    {
        public override int SetSalary(Department dept)
        {
            int result = 0;
            int sum = 0;

            if (dept.Departments != null)
            {
                foreach (var d in dept.Departments)
                {
                    foreach (var w in d.Workers)
                    {
                        if (w.Category == "Начальник отдела")
                        {
                            sum += w.SetSalary(d);
                            break;
                        }
                    }
                }

                result = sum;
                sum = (int)(sum * 0.15f);
                if (sum < 1300) sum = 1300;
                this.Salary = sum;
                result += this.Salary;
            }

            return result;
        }
    }
}
