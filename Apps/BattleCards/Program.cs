using System.Threading.Tasks;
using SUS.MvcFramework;

namespace BattleCards
{
    public static class Program
    {
        public static async Task Main()
        {
            await Host.CreateHostAsync(new Startup(), 80);
        }
    }
}
