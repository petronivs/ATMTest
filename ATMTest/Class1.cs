using System;
using System.Collections.Generic;

namespace ATMTest
{
    public class ATMClass
    {
        private Dictionary<String, ATMDrawer> CashDrawers;

        public ATMClass()
        {
            CashDrawers = new Dictionary<string, ATMDrawer>();
        }
    }

    internal class ATMDrawer
    {
        public string code { get; set; }
        public int value { get; set; }
        public int inventory { get; set; }
    }
}
