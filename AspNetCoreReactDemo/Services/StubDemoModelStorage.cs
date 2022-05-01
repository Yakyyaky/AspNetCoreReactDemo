using System.Threading.Tasks;

namespace AspNetCoreReactDemo.Services
{
    public class StubDemoModelStorage : IDemoModelStorage
    {
        public Task<bool> Update(string someOtherField)
        {
            return Task.FromResult(true);
        }

        public Task<bool> SaveChanges()
        {
            return Task.FromResult(true);
        }
    }
}