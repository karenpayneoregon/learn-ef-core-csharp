using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EntityFrameworkCoreHelpers.Classes;
using Saving.Classes;
using static Saving.Utilities.ConsoleKeysHelper;

namespace Saving
{
    public class Program
    {
        public static async Task Main()
        {
            await Task.Delay(0);
            //await BlogPostSample.CreateNewPopulateRead();
            //await BlogPostSample.DeleteAndModifyRecordIndividualContexts();
            //PauseTenSeconds("Press a key or timeout in 10 seconds");

            var test = Environment.GetEnvironmentVariables();

            Debug.WriteLine(Environment.UserName);
            //Environment.SetEnvironmentVariable("USERNAME", "Mocked");
            Debug.WriteLine(Environment.UserName);
            //Mocked.Demo();

            //**<kbd>ALT</kbd>+<kbd>ENTER</kbd>**
        }
    }


}