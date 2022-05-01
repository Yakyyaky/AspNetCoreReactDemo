using System.Threading.Tasks;

namespace AspNetCoreReactDemo.Services
{
    public interface IDemoModelStorage
    {
        public Task<bool> Update(string someOtherField);
        public Task<bool> SaveChanges();
    }
}