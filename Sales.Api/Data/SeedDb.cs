﻿using Sales.Shared.Entities;

namespace Sales.Api.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country { Name = "Colombia" });
                _context.Countries.Add(new Country { Name = "Argentina" });
                _context.Countries.Add(new Country { Name = "Chile" });

                _context.Categories.Add(new Category { Name = "Category1" });
                _context.Categories.Add(new Category { Name = "Category2" });
                _context.Categories.Add(new Category { Name = "Category3" });
                await _context.SaveChangesAsync();
            }
        }
    }
}
