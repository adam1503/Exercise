using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Data.Extensions;

namespace Data.Facts
{
    public class ExtensionsFacts
    {
        [Fact]
        public void OrderBy_Extension()
        {
            var ages = new Dictionary<string, int>(5) { { "john", 18 }, { "albert", 25 }, { "mollie", 23 }, { "steve", 19 }, { "chris", 17 } };
            var strings = new string[] { "1234", "1", "123", "12345", "12", "123" };
            Assert.Equal(new string[] { "chris", "john", "steve", "mollie", "albert" }, ages.OrderBy(x => x.Value).Select(x => x.Key));
            Assert.Equal(new string[] { "1", "12", "123", "123", "1234", "12345" }, strings.OrderBy(x => x.Length));
        }

        [Fact]
        public void ThenBy_Extension()
        {
            string[] fruits = { "grape", "passionfruit", "banana", "mango", "orange", "raspberry", "apple", "blueberry" };
            string[] expectedOrder = { "apple", "grape", "mango", "banana", "orange", "blueberry", "raspberry", "passionfruit" };
            Assert.Equal(expectedOrder, fruits.OrderBy(name => name.Length).ThenBy(fruit => fruit).Select(name => name));

            var salaries = new Dictionary<int, int>(5) { { 6, 400 }, { 5, 400 }, { 1, 700 }, { 3, 600 }, { 2, 600 } };
            Assert.Equal(new int[] { 5, 6, 2, 3, 1 }, salaries.OrderBy(x => x.Value).ThenBy(x => x.Key).Select(x => x.Key));
        }
    }
}