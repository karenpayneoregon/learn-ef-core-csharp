using System.Diagnostics;
using System.Threading.Tasks;
using Saving.Classes;
using static Saving.Utilities.ConsoleKeysHelper;

namespace Saving
{
    public class Program
    {
        public static async Task Main()
        {
            await BlogPostSample.CreateNewPopulateRead();
            PauseTenSeconds("Press a key or timeout in 10 seconds");
        }
    }
}