﻿using AutoMapper;
using ChocolateTycoon.Core;
using ChocolateTycoon.Core.DTOs;
using ChocolateTycoon.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ChocolateTycoon.Controllers.API
{
    public class FactoriesController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public FactoriesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/factories
        public IHttpActionResult GetFactories()
        {
            var factories = unitOfWork.Factories.GetFactoriesWithSupplier().ToList();

            var factoriesDto = new List<FactoryDto>();

            return Ok(Mapper.Map(factories, factoriesDto));
        }

        // PUT: /api/factories/id
        [HttpPut]
        public IHttpActionResult BreakContract(int id)
        {
            var factory = unitOfWork.Factories.GetFactoryWithStorageUnitAndSupplier(id);

            var vault = unitOfWork.Safes.GetSafe();

            if (factory == null)
                return BadRequest();

            var message = factory.BreakContract(vault);

            unitOfWork.Complete();

            return Ok(message);
        }

        // POST: /api/factories/id
        [HttpPost]
        public IHttpActionResult MakeContract(SuppliedFactoryDto suppliedFactory)
        {
            var factories = unitOfWork.Factories.GetFactoriesWithStorageUnitAndSupplier().ToList();
            var contractFactory = factories.Find(f => f.ID == suppliedFactory.Id);
            var supplier = unitOfWork.Suppliers.GetSupplier(suppliedFactory.supplierId);

            var message = "";

            if (factories == null || supplier == null)
                return BadRequest();

            else if (contractFactory.StorageUnit == null)
            {
                message = $"{contractFactory.Name} does not have a Storage Unit. Create one first!";
                return BadRequest(message);
            }

            else if (Factory.HasActiveContract(factories, contractFactory.ID))
            {
                message = $"{contractFactory.Name} already has an active Contract. Break that Contract first.";
                return BadRequest(message);
            }

            message = contractFactory.MakeContract(supplier);

            unitOfWork.Complete();

            return Ok(message);
        }


    }
}