using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace S2.Wpf.ListBoxMvvm
{
    public class ViewModel
    {
        private Repository repository;

        public ViewModel()
        {
            repository = new Repository();
            List<Employee> employees = repository.GetAll();
            Employees = new ObservableCollection<Employee>(employees);
        }

        public ObservableCollection<Employee> Employees { get; set; }

        public Employee SelectedEmployee { get; set; }

        public (bool, string) TryAddEmployee(string firstnameInput, string lastnameInput, DateTime? hireDateInput, string yearlySalaryInput)
        {
            DateTime hireDate;
            bool canParseYearlySalary = Decimal.TryParse(yearlySalaryInput, out decimal yearlySalary);
            if(String.IsNullOrWhiteSpace(firstnameInput) || String.IsNullOrWhiteSpace(lastnameInput) || String.IsNullOrWhiteSpace(yearlySalaryInput))
            {
                return (false, $"Et felt må ikke være tomt.");
            }
            else if(!canParseYearlySalary)
            {
                return (false, $"Kan ikke konvertere {yearlySalaryInput} til et beløb");
            }
            else if(hireDateInput.HasValue)
            {
                DateTime minimumHireDate = new DateTime(2005, 01, 01);
                DateTime maximumHireDate = DateTime.Now.AddMonths(3);
                hireDate = hireDateInput.Value;
                if(hireDate < minimumHireDate || hireDate > maximumHireDate)
                {
                    return (false, $"Ansættelsesdato skal være efter {minimumHireDate.ToShortDateString()} og før {maximumHireDate.ToShortDateString()} (dags dato plus tre måneder).");
                }
            }
            else
            {
                return (false, $"Der skal vælges en ansættelsesdato.");
            }

            Employee employee = new Employee() { Firstname = firstnameInput, Lastname = lastnameInput, HireDate = hireDate, YearlySalary = yearlySalary };
            repository.Add(employee);
            Update();
            return (true, String.Empty);
        }

        public void Update()
        {
            Employees.Clear();
            List<Employee> employees = repository.GetAll();
            foreach(Employee employee in employees)
            {
                Employees.Add(employee);
            }
        }

        public (bool isValid, string message) TryEditEmployee(string firstnameInput, string lastnameInput, DateTime? hireDateInput, string yearlySalaryInput)
        {
            DateTime hireDate;
            bool canParseYearlySalary = Decimal.TryParse(yearlySalaryInput, out decimal yearlySalary);
            if(String.IsNullOrWhiteSpace(firstnameInput) || String.IsNullOrWhiteSpace(lastnameInput) || String.IsNullOrWhiteSpace(yearlySalaryInput))
            {
                return (false, $"Et felt må ikke være tomt.");
            }
            else if(!canParseYearlySalary)
            {
                return (false, $"Kan ikke konvertere {yearlySalaryInput} til et beløb");
            }
            else if(hireDateInput.HasValue)
            {
                DateTime minimumHireDate = new DateTime(2005, 01, 01);
                DateTime maximumHireDate = DateTime.Now.AddMonths(3);
                hireDate = hireDateInput.Value;
                if(hireDate < minimumHireDate || hireDate > maximumHireDate)
                {
                    return (false, $"Ansættelsesdato skal være efter {minimumHireDate.ToShortDateString()} og før {maximumHireDate.ToShortDateString()} (dags dato plus tre måneder).");
                }
            }
            else
            {
                return (false, $"Der skal vælges en ansættelsesdato.");
            }

            SelectedEmployee.Firstname = firstnameInput; 
            SelectedEmployee.Lastname = lastnameInput; 
            SelectedEmployee.HireDate = hireDate; 
            SelectedEmployee.YearlySalary = yearlySalary;
            repository.Update(SelectedEmployee);
            Update();
            return (true, String.Empty);
        }
    }
}
