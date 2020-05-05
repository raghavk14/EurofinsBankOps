using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using  BankOperations;
using BankOperations.Abstracts;
using BankOperations.Implementations;
using BankOperations.Types;

namespace BankOperationsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Create Bank
                ITokenDistributor tokenDistributor = new UniformTokenDistributor();
                IBank bank = new Bank(tokenDistributor);
                //Set up few counters
                Console.WriteLine("Setting up counters. Please wait...");
                Thread.Sleep(200);
                var counters = SetupBankCounters(10);
                
                int maxCounterNumber = bank.Counters.Count;

                for (int index = maxCounterNumber + 1; index <= counters.Count; index++)
                {
                    bank.Counters.Add('C' + index.ToString(), counters[index - (maxCounterNumber + 1)]);
                }

                //Generate tokens
                VendingMachine.TokenList.Clear();

                int noOfDepositTokens = 2;
                var vendingMachine = VendingMachine.GetInstance();
                vendingMachine.GenerateTokens('D', noOfDepositTokens);

                //Assign 3 withdraw tokens to the counters.
                int noOfWithdrawTokens = 3;
                vendingMachine.GenerateTokens('W', noOfWithdrawTokens);
                
                //Assign 3 withdraw tokens to the counters.
                int noOfOtherTokens = 4;
                vendingMachine.GenerateTokens('O', noOfOtherTokens);

                Console.WriteLine("Assigning tokens to the counters. Please wait...");
                Thread.Sleep(300);
                //Segregate and assign tokens to the appropriate counters.
                bank.AssignTokensToCounters(VendingMachine.TokenList.Keys.ToList());

                Console.WriteLine("Displaying the dashboard...Please wait");
                Thread.Sleep(200);
                var actualDashBoard = bank.GetTokensGetDisplay();
                //Display dashboard to bank employee
                Console.WriteLine("========================================");
                Console.WriteLine("\tCOUNTER\t\tTOKEN");
                Console.WriteLine("========================================");
                foreach (var counter in actualDashBoard)
                {
                    Console.WriteLine(string.Format("\t{0}" + "\t\t" + "{1}",counter.Item1,counter.Item2));
                }
                Console.WriteLine("========================================");

                Console.WriteLine("Processing the tokens at the counters.Please wait...");
                //Display updated dashboard after processing few tokens
                //Get the C1 counter and process it's token
                var c1Counter = bank.Counters.Where(dCounter => dCounter.Key == "C1").Select(dCounter => dCounter.Value).SingleOrDefault();
                c1Counter.ProcessTokens();

                var c4Counter = bank.Counters.Where(dCounter => dCounter.Key == "C4").Select(dCounter => dCounter.Value).SingleOrDefault();
                c4Counter.ProcessTokens();

                Console.WriteLine("Displaying the latest dashboard of counters and their tokens. Please wait...");
                var latestDashBoard = bank.GetTokensGetDisplay();
                //Display dashboard to bank employee
                Console.WriteLine("========================================");
                Console.WriteLine("\tCOUNTER\t\tTOKEN");
                Console.WriteLine("========================================");
                foreach (var counter in latestDashBoard)
                {
                    Console.WriteLine(string.Format("\t{0}" + "\t\t" + "{1}", counter.Item1, counter.Item2));
                }
                Console.WriteLine("========================================");

                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        private static IList<Counter> SetupBankCounters(int capacity)
        {
            Counter depositCounter1 = new MonetoryTxnCounter(TokenType.Deposit, capacity);
            Counter withdrawCounter1 = new MonetoryTxnCounter(TokenType.Withdraw, capacity);
            Counter withdrawCounter2 = new MonetoryTxnCounter(TokenType.Withdraw, capacity);
            Counter nonMonetaryCounter1 = new NonMonetoryTxnCounter(TokenType.Other, capacity);
            Counter nonMonetaryCounter2 = new NonMonetoryTxnCounter(TokenType.Other, capacity);

            return new List<Counter>
            {
                depositCounter1,
                withdrawCounter1,
                withdrawCounter2,
                nonMonetaryCounter1,
                nonMonetaryCounter2
            };

        }
    }
}
