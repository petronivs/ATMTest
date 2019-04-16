using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMTest;

namespace ATMConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ATMClass myATM = new ATMClass();

            while (true)
            {
                string inputString = Console.ReadLine();
                string[] inputArguments = inputString.Split(' ');
                switch (inputArguments[0])
                {
                    case "I":
                        DisplayInventory(myATM.Inventory(inputArguments.Skip(1).ToArray()).ToList());
                        break;
                    case "R":
                        myATM.Restock();
                        DisplayInventory(myATM.Inventory().ToList());
                        break;
                    case "W":
                        try
                        {
                            int withdrawAmount = 0;
                            int amountReceived = 0;
                            if (int.TryParse(inputArguments[1].Trim('$'), out withdrawAmount))
                            {
                                amountReceived = myATM.Withdraw(withdrawAmount);
                            }
                            Console.WriteLine("Success: Dispensed ${0}", amountReceived.ToString());
                            Console.WriteLine("Machine balance:");
                            DisplayInventory( myATM.Inventory().ToList());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Failure: {0}", ex.Message);
                        }
                        break;
                        
                    default:
                        Console.WriteLine("Failure: Invalid Command");
                        break;
                }

            }

        }

        private static void DisplayInventory(List<(string, int)> inventoryList)
        {
            foreach ((string, int) drawer in inventoryList)
            {
                Console.WriteLine("{0} - {1}", drawer.Item1, drawer.Item2.ToString());
            }
        }
    }
}
