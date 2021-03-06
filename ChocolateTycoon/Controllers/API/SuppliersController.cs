﻿using AutoMapper;
using ChocolateTycoon.Core;
using ChocolateTycoon.Core.DTOs;
using System.Web.Http;

namespace ChocolateTycoon.Controllers.API
{
    public class SuppliersController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public SuppliersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: /Api/Suppliers/Id
        [HttpGet]
        public IHttpActionResult GetSupplier(int id)
        {
            var supplierDb = unitOfWork.Suppliers.GetSupplierWithFactories(id);

            return Ok(Mapper.Map<SupplierDto>(supplierDb));
        }
    }
}
