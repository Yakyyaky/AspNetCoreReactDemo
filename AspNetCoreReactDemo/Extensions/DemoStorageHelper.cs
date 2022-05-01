using System.Threading.Tasks;
using AspNetCoreReactDemo.Services;

namespace AspNetCoreReactDemo.Extensions
{
    public static class DemoStorageHelper
    {
        public static async Task<bool> SaveData(this IDemoModelStorage storage, string data)
        {
            var result = await storage.Update(data);
            if (!result) return false;

            return await storage.SaveChanges();
        }
    }
}
