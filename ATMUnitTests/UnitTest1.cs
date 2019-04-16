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
        public void TestCreateSize()
        {
            ATMClass newATM = new ATMClass();
            List<(string, int)> ATMinventory = newATM.Inventory().ToList();
            Assert.AreEqual(ATMinventory.Count, 6);

        }

        [TestMethod]
        public void TestCreateList()
        {
            ATMClass newATM = new ATMClass();
            List<(string, int)> targetList = new List<(string, int)> {
                ("$100", 10),
                ("$50", 10),
                ("$20", 10),
                ("$10", 10),
                ("$5", 10),
                ("$1", 10)
            };
            List<(string, int)> ATMinventory = newATM.Inventory().ToList();
            
            CollectionAssert.AreEqual(targetList, ATMinventory, Comparer<(string, int)>.Create((x,y)=>x.Item1 == y.Item1 && x.Item2 == y.Item2 ? 0 : -1));
        }

        [TestMethod]
        public void TestWithdraw1()
        {
            ATMClass newATM = new ATMClass();
            int withdrawAmount = newATM.Withdraw(208);
            Assert.AreEqual(withdrawAmount, 208);

            List<(string, int)> ATMinventory = newATM.Inventory().ToList();
            CollectionAssert.AreEqual(new List<(string, int)> {
                                            ("$100", 8),
                                            ("$50", 10),
                                            ("$20", 10),
                                            ("$10", 10),
                                            ("$5", 9),
                                            ("$1", 7)
                                        }
                , ATMinventory, Comparer<(string, int)>.Create((x, y) => x.Item1 == y.Item1 && x.Item2 == y.Item2 ? 0 : -1));
        }
    }
}
