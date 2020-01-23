﻿using System;
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
    }
}
