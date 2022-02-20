using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelationalLevel1.Data;
using RelationalLevel1.Models;

namespace RelationalLevel1ConsoleApp.Classes
{

    public class ReadOperations
    {
        public static async Task ReadPeople()
        {

            Debug.WriteLine(new string('_', 50));
            Debug.WriteLine($"✔ {nameof(ReadPeople)}");
            Debug.WriteLine(new string('_', 50));

            await using var context = new PersonContext();

            List<Person> people = await context
                .People
                .Include(person => person.Addresses)
                .Include(person => person.ContactDevices)
                .ThenInclude(contactDevice => contactDevice.DeviceType)
                .ToListAsync();

            foreach (var person in people)
            {
                Debug.WriteLine($"{person.Id} {person.FirstName} {person.LastName}");
                Debug.WriteLine("Addresses");
                foreach (var address in person.Addresses)
                {
                    Debug.WriteLine($"\t{address.Id} {address.Street} {address.PostalCode}");
                }

                Debug.WriteLine("Devices");
                foreach (var device in person.ContactDevices)
                {
                    Debug.WriteLine($"\t{device.DeviceTypeId}  {device.DeviceType} {device.Value}");
                }

                var homePhone = person.ContactDevices.FirstOrDefault(x => x.DeviceTypeId == 1);
                Debug.WriteLine(homePhone is not null ? "\tHas home phone" : "\tHas no home phone");

                var workPhone = person.ContactDevices.FirstOrDefault(x => x.DeviceTypeId == 2);
                Debug.WriteLine(workPhone is not null ? "\tHas work phone" : "\tHas no work phone");
                
                Debug.WriteLine("----------------------");
            }

            Debug.WriteLine("");


        }

    }
}
