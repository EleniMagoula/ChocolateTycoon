﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Models
{
    public class Factory
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Please enter at least 3 characters")]
        public string Name { get; set; }
        public byte Level { get; private set; }
        private const int _createCost = 2000;
        public static int CreateCost => _createCost;

        public int? SupplierId { get; set; }

        public ProductionUnit ProductionUnit { get; set; }
        public StorageUnit StorageUnit { get; set; }
        public List<Employee> Employees { get; set; }
        public Supplier Supplier { get; set; }


        public Factory()
        {
            Level = 1;
            Employees = new List<Employee>();
        }

        public void Produce(MainStorage mainStorage)
        {
            var materialsNeeded = ProductionUnit.MaterialsNeeded();
            var materialsSuffice = StorageUnit.MaterialsSuffice(materialsNeeded);

            if (!ProductionUnit.ProducedDailyProduction)
            {
                if (PersonelSuffice() && materialsSuffice)
                {
                    var products = ProductionUnit.DailyProduction();

                    mainStorage.newProducts.AddRange(products);

                    ProductionUnit.ProducedDailyProduction = true;
                }
                else if (!PersonelSuffice())
                    Message.SetErrorMessage(MessageEnum.PersonelError);
                else
                    Message.SetErrorMessage(MessageEnum.RawMaterialsError);
            }
            else
                Message.SetErrorMessage(MessageEnum.ProductionTurnError);
        }

        //checks if the factory personel meets the required minimum for the factory to operate
        public bool PersonelSuffice()
        {
            var managersEmployed = Employees.Where(e => e.Position == EmployeePosition.FactoryManager).Count();
            var employeesEmployed = Employees.Where(e => e.Position == EmployeePosition.ProductionEngineer).Count();

            if (Level == 1)
            {
                if (managersEmployed >= 1 && employeesEmployed >= 2)
                    return true;
            }
            else if (Level == 2)
            {
                if (managersEmployed >= 2 && employeesEmployed >= 4)
                    return true;
            }

            return false;
        }

        // Breaks the Contract with an active Supplier and returns a message
        public string BreakContract()
        {
            if (Supplier != null)
            {
                var supplierName = Supplier.Name;
                Supplier = null;
                StorageUnit.ResetSupplier();

                return $"{Name} Factory broke it's Contract with {supplierName}. You got a penalty charge..";
            }

            return "Something went wrong. Try again";
        }

        // Makes a new contract with the Supplier it takes as a parameter
        // and returns a message on success
        public string MakeContract(Supplier supplier)
        {
            Supplier = supplier;

            return $"A Contract has been made between {supplier.Name} and {Name} Factory.";
        }

        // Checks whether a Factory from a list of Factories, has an active Contract with
        // any Supplier and returns a boolean value
        public static bool HasActiveContract(List<Factory> factories, int factoryId)
        {
            var suppliedFactories = factories.Where(f => f.Supplier != null).ToList();
            var hasSupplier = suppliedFactories.Exists(f => f.ID == factoryId);

            if (suppliedFactories != null)
                if (hasSupplier)
                    return true;

            return false;
        }
    }
}