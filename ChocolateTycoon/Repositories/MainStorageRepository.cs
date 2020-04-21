﻿using ChocolateTycoon.Data;
using ChocolateTycoon.Models;
using System.Linq;

namespace ChocolateTycoon.Repositories
{
    public class MainStorageRepository : IMainStorageRepository
    {
        private readonly ApplicationDbContext _db;

        public MainStorageRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public MainStorage GetMainStorage()
        {
            return _db.MainStorages.SingleOrDefault(ms => ms.ID == 1);
        }
    }
}