﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using ChocolateTycoon.Models;

namespace ChocolateTycoon.ViewModels
{
    public class MainStorageViewModel
    {
        public MainStorage MainStorage { get; set; }        
        public IDictionary<string, int> availableStorage = new Dictionary<string, int>();


        public List<Chocolate> Chocolates { get; set; }
        public IDictionary<string, int> availableChocolates = new Dictionary<string, int>();

        public List<string> enumNames = GetChocolateDisplayNames();

        public MainStorageViewModel()
        {
            Chocolates = new List<Chocolate>();
        }

        private static List<string> GetChocolateDisplayNames()
        {
            var values = Enum.GetValues(typeof(ChocolateType));
            var names = new List<string>();

            foreach (var item in values)
            {
                names.Add(item.GetType()
                    .GetMember(item.ToString())
                    .First()
                    .GetCustomAttribute<DisplayAttribute>()
                    .Name);
            }

            return names;
        }
    }
}