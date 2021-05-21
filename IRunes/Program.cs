using System.Threading.Tasks;
using SUS.MvcFramework;

namespace IRunes
{
    public class Program
    {
        public static async Task Main()
        {
            await Host.CreateHostAsync(new Startup(), 80);
        }
    }
}
