using System;
using System.Threading.Tasks;

namespace PowershellScripts
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Run script");

                await Powershell.StartAsync();

                Console.WriteLine("Finished script");
            }
            catch (Exception exception)
            {
                Console.WriteLine("----------------");
                Console.WriteLine($"Error : {exception}");
                Console.WriteLine("----------------");
            }
        }
    }
}
