﻿using ChocolateTycoon.Models;
using ChocolateTycoon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ChocolateTycoon.Data;

namespace ChocolateTycoon.Controllers
{
    public class ProductionUnitController : Controller
    {
        ApplicationDbContext db;

        public ProductionUnitController()
        {
            db = new ApplicationDbContext();
        }

        //// GET: ProductionUnit/Create
        //public ActionResult Create(int id)
        //{
        //    var factory = db.Factories
        //        .Include(f => f.ProductionUnit)
        //        .SingleOrDefault(f => f.ID == id);

        //    return View(factory);
        //}

        // POST: ProductionUnit/Create
        [HttpPost, ActionName("Create")]
        public ActionResult CreatePost(Factory factory)
        {
            ProductionUnit productionUnit = new ProductionUnit
            {
                FactoryID = factory.ID,
                MaxProductionPerDay = 200
            };

            db.ProductionUnits.Add(productionUnit);
            db.SaveChanges();

            return RedirectToAction("Index", "Factory", new { id = factory.ID });
        }

    }
}