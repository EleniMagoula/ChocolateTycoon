﻿using ChocolateTycoon.Core.Models;
using ChocolateTycoon.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ChocolateTycoon.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _db.Employees;
        }

        public IEnumerable<Employee> GetEmployeesBasedOnStore(int storeId)
        {
            return _db.Employees
                .Where(e => e.StoreID == storeId);
        }

        public IEnumerable<Employee> GetEmployeesWithStoresAndFactories()
        {
            return _db.Employees
                .Include(e => e.Factory)
                .Include(e => e.Store);
        }

        public Employee GetEmployee(int id)
        {
            return _db.Employees
                .SingleOrDefault(e => e.Id == id);
        }

        public void Add(Employee employee)
        {
            _db.Employees.Add(employee);
        }

        public void AddMany(List<Employee> employees)
        {
            _db.Employees.AddRange(employees);
        }

        public void Remove(Employee employee)
        {
            _db.Employees.Remove(employee);
        }

        public void RemoveMany(List<Employee> employees)
        {
            _db.Employees.RemoveRange(employees);
        }
    }
}