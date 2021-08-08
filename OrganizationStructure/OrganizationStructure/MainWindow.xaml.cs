using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace OrganizationStructure
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод рекурсивного вывода структуры департамента
        /// </summary>
        /// <param name="dept">Департамент</param>
        /// <param name="item">Элемент древовидного списка</param>
        private void GetSubDepartments(Department dept, TreeViewItem item)
        {
            if (dept.Departments != null)
            {
                foreach (var d in dept.Departments)
                {
                    TreeViewItem subDeptItem = new TreeViewItem();
                    subDeptItem.Header = d.Name;
                    GetSubDepartments(d, subDeptItem);
                    item.Items.Add(subDeptItem);
                }
            }

            foreach (var w in dept.Workers)
            {
                TreeViewItem subItem = new TreeViewItem();
                subItem.Header = "Имя: " + w.FirstName + ";  Фамилия: " + w.LastName + ";  Возраст: " + w.Age + ";  Должность: " + w.Category + ";  Зарплата: " + w.Salary;
                item.Items.Add(subItem);
            }
        }

        /// <summary>
        /// Метод вывода структуры организации из JSON-файла
        /// </summary>
        private void ShowStructure()
        {
            Department dept;
            using (StreamReader sr = new StreamReader("Organization.json"))
            {
                while (!sr.EndOfStream)
                {
                    string json = sr.ReadLine();
                    dept = JsonConvert.DeserializeObject<Department>(json);
                    TreeViewItem item = new TreeViewItem();
                    item.Header = dept.Name;
                    GetSubDepartments(dept, item);
                    OrganizationView.Items.Add(item);             
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Department.CreateOrganization();
            ShowStructure();
        }
    }
}
