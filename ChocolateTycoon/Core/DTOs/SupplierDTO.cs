﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChocolateTycoon.Core.DTOs
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OfferAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal PricePerKilo { get; set; }
        public int ShippedAmount { get; set; }

        public List<SuppliedFactoryDto> Factories { get; set; }

        public SupplierDto()
        {
            Factories = new List<SuppliedFactoryDto>();
        }
    }
}