using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCoreHelpers.Classes
{
    public class Mocked
    {
        public static List<Person> People => new()
        {
            new () { Identifier = 1, FirstName = "A" },
            new () { Identifier = 2, FirstName = "B" },
        };

        public static void Demo()
        {
            IEnumerable<Person> query = from p in People select p;
            Worker.CompareValueTexture<Person>(People);

        }
    }
}