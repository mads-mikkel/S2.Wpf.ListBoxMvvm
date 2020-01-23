using System;
using System.Collections.Generic;
using System.Text;

namespace S2.Wpf.ListBoxMvvm
{
    public class Repository
    {
        private List<Employee> employees;

        public Repository()
        {
            employees = new List<Employee>() {
                new Employee() {Firstname = "Brian", Lastname = "Jørgensen", HireDate = new DateTime(01,08,2016), YearlySalary = 10000m},
                new Employee() {Firstname = "Ole", Lastname = "Bay Jensen", HireDate = new DateTime(01,01,2005), YearlySalary = 12000m}
            };
        }

        public List<Employee> GetAll()
        {
            return employees;
        }

        public void Add(Employee employee)
        {
            employees.Add(employee);
        }
    }
}
