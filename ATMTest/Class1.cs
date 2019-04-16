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
                ( "$100", new ATMDrawer(100, 10 ) ),
                ( "$50", new ATMDrawer(50, 10 ) ),
                ( "$20", new ATMDrawer(20, 10 ) ),
                ( "$10", new ATMDrawer(10, 10 ) ),
                ( "$5", new ATMDrawer(5, 10 ) ),
                ("$1", new ATMDrawer(1, 10 ) )
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
            List<(string, ATMDrawer)> backupATMState = Copy(CashDrawers);

            try
            {
                // start making passes through inventory
                while (amount > 0)
                {
                    int tempReturnAmount = returnAmount;

                    // see if we can pull a bill out of a drawer to return.
                    // TODO: ensure this is sorted big to small.
                    foreach ((string, ATMDrawer) drawer in CashDrawers)
                    {
                        // enough money in drawer and enough bills to pull one out?
                        if(drawer.Item2.value <= amount && drawer.Item2.inventory > 0)
                        {
                            tempReturnAmount += drawer.Item2.value;
                            drawer.Item2.inventory--;
                            amount -= drawer.Item2.value;
                            break;
                        }
                    }

                    if (tempReturnAmount == returnAmount) { throw new InvalidOperationException("insufficient funds"); }
                    else { returnAmount = tempReturnAmount; }
                }
            }
            catch(InvalidOperationException ex)
            {
                // roll back the transaction
                CashDrawers = Copy(backupATMState);
                throw;
            }

            return returnAmount;
        }

        private List<(string, ATMDrawer)> Copy(List<(string, ATMDrawer)> cashDrawers)
        {
            List<(string, ATMDrawer)> returnList = new List<(string, ATMDrawer)>();

            foreach((string, ATMDrawer) newDrawer in cashDrawers)
            {
                returnList.Add((newDrawer.Item1, new ATMDrawer(newDrawer.Item2.value, newDrawer.Item2.inventory)));
            }
            return returnList;
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

        public IEnumerable<(string, int)> Inventory(string[] drawers)
        {
            foreach(string drawer in drawers)
            {
                (string, ATMDrawer) activeDrawer = CashDrawers.Find(foundDrawer => foundDrawer.Item1 == drawer);
                yield return (activeDrawer.Item1, activeDrawer.Item2.inventory);
            }
        }
    }

    /// <summary>
    /// a drawer of banknotes
    /// </summary>
    internal class ATMDrawer
    {
        public ATMDrawer(int value, int inventory)
        {
            this.value = value;
            this.inventory = inventory;
        }

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
