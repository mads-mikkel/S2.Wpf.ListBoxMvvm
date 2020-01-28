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
                new Employee() {Id = new Random().Next(1, Int32.MaxValue), Firstname = "Brian", Lastname = "Jørgensen", HireDate = new DateTime(2016,08,01), YearlySalary = 10000m},
                new Employee() {Id = new Random().Next(1, Int32.MaxValue),Firstname = "Ole", Lastname = "Bay Jensen", HireDate = new DateTime(2005,01,01), YearlySalary = 12000m}
            };
        }

        public List<Employee> GetAll()
        {
            return employees;
        }

        public void Add(Employee employee)
        {
            int newId = new Random().Next(1, Int32.MaxValue);
            employee.Id = newId;
            employees.Add(employee);
        }

        public void Update(Employee employee)
        {
            Employee employeeToUpdate = employees.Find(e => e.Id == employee.Id);
            employeeToUpdate.Firstname = employee.Firstname;
            employeeToUpdate.Lastname = employee.Lastname;
            employeeToUpdate.HireDate = employee.HireDate;
            employeeToUpdate.YearlySalary = employee.YearlySalary;
        }
    }
}
