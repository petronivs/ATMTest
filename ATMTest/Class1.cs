using System;
using System.Collections.Generic;

namespace ATMTest
{
    public class ATMClass
    {
        private List<(String, ATMDrawer)> CashDrawers;

        private int TotalFunds
        {
            get
            {
                int returnAmount = 0;
                foreach((string, ATMDrawer) drawer in CashDrawers)                
                {
                    returnAmount += drawer.Item2.TotalValue;
                }

                return returnAmount;
            }
        }

        public ATMClass()
        {
            this.Restock();
        }

        /// <summary>
        /// reset cash drawers.
        /// </summary>
        public void Restock()
        {
            CashDrawers = new List<(string, ATMDrawer)>
            {
                ( "$100", new ATMDrawer{value=100, inventory=10 } ),
                ( "$50", new ATMDrawer{value=50, inventory=10 } ),
                ( "$20", new ATMDrawer{value=20, inventory=10 } ),
                ( "$10", new ATMDrawer{value=10, inventory=10 } ),
                ( "$5", new ATMDrawer{value=5, inventory=5 } ),
                ("$1", new ATMDrawer{value=1, inventory=5 } )
            };

            return;
        }

        /// <summary>
        /// withdraw money from ATM.  Just return amount for now.
        /// </summary>
        /// <param name="amount"> amount withdrawn</param>
        /// <returns></returns>
        public int Withdraw(int amount)
        {
            if (amount > TotalFunds) throw new Exception("Insufficient Funds");
            int returnAmount = 0;
            try
            {
                // start making passes through inventory
                while(amount > 0)
                {
                    int tempReturnAmount = returnAmount;

                    // see if we can pull a bill out of a drawer to return.
                    // TODO: ensure this is sorted big to small.
                    foreach((string, ATMDrawer) drawer in CashDrawers)
                    {
                        // enough money in drawer and enough bills to pull one out?
                        if(drawer.Item2.value < amount && drawer.Item2.inventory > 0)
                        {
                            tempReturnAmount += drawer.Item2.value;
                            drawer.Item2.inventory--;
                            break;
                        }
                    }

                    if (tempReturnAmount == returnAmount) {throw new Exception("insufficient bills for amount requested");}
                    else { returnAmount = tempReturnAmount; }
                }
            }
            catch(Exception ex)
            {
                throw;
            }

            return returnAmount;
        }

        /// <summary>
        /// list the inventory in the ATM.
        /// make sure to cast to a list when calling. 
        /// </summary>
        /// <returns>a tuple list listing the label and value in each drawer</returns>
        public IEnumerable<(string, int)> Inventory()
        {
            foreach((string, ATMDrawer) drawer in CashDrawers)
            {
                yield return (drawer.Item1, drawer.Item2.inventory);
            }
        }
    }

    /// <summary>
    /// a drawer of banknotes
    /// </summary>
    internal class ATMDrawer
    {
        // TODO: add internationalization for foreign banknotes
        /// <summary>
        /// the value of each bill in the drawer
        /// </summary>
        public int value { get; set; }
        /// <summary>
        /// the number of bills in the drawer
        /// </summary>
        public int inventory { get; set; }
        /// <summary>
        /// the total value of this drawer.
        /// </summary>
        public int TotalValue { get { return value * inventory; } }
    }
}
