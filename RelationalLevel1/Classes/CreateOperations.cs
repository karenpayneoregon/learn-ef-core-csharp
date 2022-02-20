using System.Diagnostics;
using System.Threading.Tasks;
using RelationalLevel1.Data;

namespace RelationalLevel1.Classes
{
    public class CreateOperations
    {

        public static async Task NewPeopleDatabase()
        {
            Debug.WriteLine($"✔ {nameof(NewPeopleDatabase)}");

            await using var context = new PersonContext();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
    }
}
