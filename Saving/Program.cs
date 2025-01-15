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
            await BlogPostSample.CreateNewPopulateRead();
            //await BlogPostSample.DeleteAndModifyRecordIndividualContexts();
            //PauseTenSeconds("Press a key or timeout in 10 seconds");


            //Mocked.Demo();


            await  BlogPostSample.UpdateExisting();
        }
    }


}