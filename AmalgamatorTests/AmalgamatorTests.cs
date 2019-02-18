using Microsoft.VisualStudio.TestTools.UnitTesting;
using Amalgamator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amalgamator.Tests
{
    [TestClass()]
    public class AmalgamatorTests
    {
        [TestMethod()]
        public void amalgamateTest()
        {
            List<string> partials = new List<string> {
                "ell that en",
                "at e",
                "t ends well",
                "hat end",
                "all is well"
            };
            var amalgamator = new Amalgamator(partials);
            var result = amalgamator.amalgamate();
            Assert.AreEqual("all is well that ends well", result);
        }
    }
}