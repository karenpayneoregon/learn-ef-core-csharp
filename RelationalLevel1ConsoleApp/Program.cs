using System;
using System.Diagnostics;
using System.Threading.Tasks;
using RelationalLevel1.Classes;
using RelationalLevel1ConsoleApp.Classes;

namespace RelationalLevel1ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {

            //await CreatePopulateRead();

            var (success, exception) = await WorkWithPopulatedDatabase();
            if (exception is not null)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Use this after <seealso cref="CreatePopulateRead"/> and no more changes are needed
        /// </summary>
        /// <returns>success, exception on any thrown exceptions</returns>
        private static async Task<(bool, Exception localException)> WorkWithPopulatedDatabase()
        {
            try
            {
                await ReadOperations.ReadPeople();
                return (true, null);
            }
            catch (Exception localException)
            {
                return (false, localException);
            }
        }

        private static async Task CreatePopulateRead()
        {
            await CreateOperations.NewPeopleDatabase();
            await PopulateOperations.People();
            await ReadOperations.ReadPeople();
        }
    }
}
