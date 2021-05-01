using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Models.Common;
using Backend.Models.ProductBalanceEntity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.WarehouseEntity
{
    public class WarehouseSeed : ISeeder
    {
        private readonly ApiContext _context;
        private readonly Random _rnd;

        public WarehouseSeed(ApiContext context)
        {
            _context = context;
            _rnd = new Random(2021);
        }

        public void EnsureCreated()
        {
            if (!ShouldSeed()) return;

            for (int i = 1; i <= 3; i++)
            {
                var warehouse = new Warehouse
                {
                    Id = i,
                    Address = $"Warehouse Str. {i * 10}",
                    City = "Kaunas",
                    Products = PopulateProducts(i)
                };

                _context.Add(warehouse);
            }

            _context.SaveChanges();
        }

        private bool ShouldSeed()
        {
            var warehouse = _context.Warehouses.IgnoreQueryFilters().FirstOrDefault(p => p.Id == 1);
            return warehouse == null;
        }

        private List<ProductBalance> PopulateProducts(int id)
        {
            int medicamentId = _rnd.Next(0, _context.Medicaments.Count());

            var products = new List<ProductBalance>
            {
                new()
                {
                    Amount = _rnd.Next(5, 20),
                    ExpirationDate = DateTime.Today.AddYears(1),
                    MedicamentId = medicamentId,
                    WarehouseId = id
                }
            };

            return products;
        }
    }
}
