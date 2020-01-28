using System;
using System.Collections.Generic;
using System.Text;

namespace S2.Wpf.ListBoxMvvm
{
    public class Employee
    {

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime HireDate { get; set; }
        public decimal YearlySalary { get; set; }

        public string Fullname => $"{Firstname} {Lastname}";
    }
}