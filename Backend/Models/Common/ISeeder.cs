using System.Threading.Tasks;

namespace Backend.Models.Common
{
    public interface ISeeder
    {
        public Task EnsureCreated();
    }
}
