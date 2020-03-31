﻿using ChocolateTycoon.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChocolateTycoon.Controllers
{
    public class SafeController : Controller
    {
        private ApplicationDbContext db;

        public SafeController()
        {
            db = new ApplicationDbContext();
        }

        
        // GET: Safe
        public ActionResult Vault()
        {
            var vault = db.Safes.Where(s => s.ID == 1).Single();

            return PartialView("_VaultPartial", vault);
        }
    }
}