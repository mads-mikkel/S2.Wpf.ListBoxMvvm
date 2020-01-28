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
                new Employee() {Firstname = "Brian", Lastname = "Jørgensen", HireDate = new DateTime(2016,08,01), YearlySalary = 10000m},
                new Employee() {Firstname = "Ole", Lastname = "Bay Jensen", HireDate = new DateTime(2005,01,01), YearlySalary = 12000m}
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

        internal void Update(Employee selectedEmployee)
        {
            
        }
    }
}
