using A.S.H.L.E.E._alfa.Core;

namespace A.S.H.L.E.E._alfa
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "ASHLEE ARMOR OS";

            AshleeSystem ashlee = new AshleeSystem();

            await ashlee.InitializeAsync();

            await ashlee.StartAsync();
        }
    }
}
