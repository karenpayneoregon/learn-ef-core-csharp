using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindContacts.Data;
using NorthWindContactsUnitTest.Base;

namespace NorthWindContactsUnitTest
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        [TestMethod]
        [TestTraits(Trait.PlaceHolder)]
        public async Task GetContactsCount()
        {
            await using var context = new NorthContext();
            var results = await context.Contacts.CountAsync();
            Assert.AreEqual(results, 91);
        }
    }
}
