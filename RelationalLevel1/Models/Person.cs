using System.Collections.Generic;

namespace RelationalLevel1.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Address> Addresses { get; set; }
        public List<ContactDevice> ContactDevices { get; set; }
        public override string ToString() => $"{FirstName} {LastName}";
    }
}