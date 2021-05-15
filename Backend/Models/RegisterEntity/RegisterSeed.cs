using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.RegisterEntity
{
    public class RegisterSeed : ISeeder
    {
        private readonly ApiContext _context;

        public RegisterSeed(ApiContext context)
        {
            _context = context;
        }

        public async Task EnsureCreated()
        {
            if (!ShouldSeed()) return;


            var pharmacy = _context.Pharmacies.First(p => p.Id == 1);

            await _context.Registers.AddAsync(new Register
            {
                Id = 1,
                Cash = 0.00M,
                Pharmacy = pharmacy
            });

            await _context.SaveChangesAsync();
        }

        private bool ShouldSeed()
        {
            var user = _context.Registers.IgnoreQueryFilters().FirstOrDefault(m => m.Id == 1);
            return user == null;
        }
    }
}
