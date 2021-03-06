﻿using ChocolateTycoon.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChocolateTycoon.Core.ViewModels
{
    public class StoreEmployeesViewModel
    {
        public Store Store { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        public int _total;
        public int _manager;
        public int _sales;


        public void GetEmployees()
        {
            var total = Employees.Count();
            var manager = Employees.Where(e => e.Position == EmployeePosition.StoreManager).Count();
            var sales = Employees.Where(e => e.Position == EmployeePosition.SalesExpert).Count();

            _total = total;
            _manager = manager;
            _sales = sales;
        }
    }
}