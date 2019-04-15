using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ATMTest;
using System.Linq;
using System.Collections.Generic;

namespace ATMUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreate()
        {
            ATMClass newATM = new ATMClass();
            List<(string, int)> ATMinventory = newATM.Inventory().ToList();
            Assert.AreEqual(ATMinventory.Count, 6);

        }
    }
}
