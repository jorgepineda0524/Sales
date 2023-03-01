using Sales.Shared.Entities;

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
                _context.Countries.Add(new Country 
                { 
                    Name = "Colombia" ,
                    States = new List<State>
                    {
                        new State 
                        { 
                            Name = "Antioquia",
                            Cities = new List<City>
                            {
                                new City { Name = "Medellín" },
                                new City { Name = "Itagüi" },
                                new City { Name = "Sabaneta" }
                            }
                        },
                        new State
                        {
                            Name = "Bogotá",
                            Cities = new List<City>
                            {
                                new City { Name = "Usaquen" },
                                new City { Name = "Chapinero" },
                                new City { Name = "Bosa" }
                            }
                        },
                    }
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States = new List<State>
                    {
                        new State
                        {
                            Name = "Florida",
                            Cities = new List<City>
                            {
                                new City { Name = "Orlando" },
                                new City { Name = "Miami" },
                                new City { Name = "Tampa" }
                            }
                        },
                        new State
                        {
                            Name = "Texas",
                            Cities = new List<City>
                            {
                                new City { Name = "Houston" },
                                new City { Name = "San Antionio" },
                                new City { Name = "Dallas" }
                            }
                        },
                    }
                });

                _context.Categories.Add(new Category { Name = "Moda" });
                _context.Categories.Add(new Category { Name = "Calzado" });
                _context.Categories.Add(new Category { Name = "Tecnología" });
                await _context.SaveChangesAsync();
            }
        }
    }
}
