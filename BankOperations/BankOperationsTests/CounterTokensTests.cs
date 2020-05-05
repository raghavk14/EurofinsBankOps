using BankOperations.Abstracts;
using BankOperations.Implementations;
using BankOperations.Types;
using System.Collections.Generic;
using System.Linq;
using BankOperations;
using Xunit;

namespace BankOperationsTests
{
    public class CounterTokensTests
    {
        private IList<Counter> SetupDepositCounters(int capacity)
        {
            Counter depositCounter1 = new MonetoryTxnCounter(TokenType.Deposit, capacity);
            Counter depositCounter2 = new MonetoryTxnCounter(TokenType.Deposit, capacity);


            return new List<Counter>
            {
                depositCounter1,
                depositCounter2
            };

        }

        private IList<Counter> SetupWithdrawCounters(int capacity)
        {
            Counter withdrawCounter1 = new MonetoryTxnCounter(TokenType.Withdraw, capacity);
            Counter withdrawCounter2 = new MonetoryTxnCounter(TokenType.Withdraw, capacity);
            Counter withdrawCounter3 = new MonetoryTxnCounter(TokenType.Withdraw, capacity);

            return new List<Counter>
            {
                withdrawCounter1,
                withdrawCounter2,
                withdrawCounter3
            };

        }

      

        [Fact]
        public void Evenly_Distribute_OddnumberOfTokens_To_EvennumberOfCounters_Test()
        {
            //Arrange
            //create odd no. of tokens
            VendingMachine.TokenList.Clear();
            ITokenDistributor tokenDistributor = new UniformTokenDistributor();
            IBank bank = new Bank(tokenDistributor);

            //create even no. of counters
            var counters = SetupDepositCounters(10);

            //Act
            int maxCounterNumber = bank.Counters.Count;

            for (int index = maxCounterNumber + 1; index <= counters.Count; index++)
            {
                bank.Counters.Add('C' + index.ToString(), counters[index - (maxCounterNumber + 1)]);
            }

            //Generate Tokens
            var vendingMachine = VendingMachine.GetInstance();
            int noOfTokens = 9;
            vendingMachine.GenerateTokens('D', noOfTokens);
            
            //Act
            //Assign tokens to the counters.
            bank.AssignTokensToCounters(VendingMachine.TokenList.Keys.ToList());

            //Assert
            //We expect C1 counter queue must have 5 tokens and C2 counter must have 4 tokens
            var c1Counter = bank.Counters["C1"];
            Assert.Equal(5,c1Counter.CounterQueue.Count);

            var c2Counter = bank.Counters["C2"];
            Assert.Equal(4, c2Counter.CounterQueue.Count);


        }

        [Fact]
        public void Evenly_Distribute_EvennumberOfTokens_To_EvennumberOfCounters_Test()
        {
            //Arrange
            //create odd no. of tokens
            VendingMachine.TokenList.Clear();
            ITokenDistributor tokenDistributor = new UniformTokenDistributor();
            IBank bank = new Bank(tokenDistributor);

            //create even no. of counters
            var counters = SetupDepositCounters(10);

            int maxCounterNumber = bank.Counters.Count;

            for (int index = maxCounterNumber + 1; index <= counters.Count; index++)
            {
                bank.Counters.Add('C' + index.ToString(), counters[index - (maxCounterNumber + 1)]);
            }

            var vendingMachine = VendingMachine.GetInstance();
            int noOfTokens = 12;
            vendingMachine.GenerateTokens('D', noOfTokens);
            
            //Act
            //Assign tokens to the counters.
            bank.AssignTokensToCounters(VendingMachine.TokenList.Keys.ToList());

            //Assert
            //We expect C1 counter queue must have 5 tokens and C2 counter must have 4 tokens
            var c1Counter = bank.Counters["C1"];
            Assert.Equal(6, c1Counter.CounterQueue.Count);

            var c2Counter = bank.Counters["C2"];
            Assert.Equal(6, c2Counter.CounterQueue.Count);

        }

        [Fact]
        public void Evenly_Distribute_EvennumberOfTokens_To_OddnumberOfCounters_Test()
        {
            //Arrange
            //create odd no. of tokens
            VendingMachine.TokenList.Clear();
            ITokenDistributor tokenDistributor = new UniformTokenDistributor();
            IBank bank = new Bank(tokenDistributor);
            //create odd no. of counters
            var counters = SetupWithdrawCounters(10);

            //Act
            int maxCounterNumber = 0;

            for (int index = maxCounterNumber + 1; index <= counters.Count; index++)
            {
                bank.Counters.Add('C' + index.ToString(), counters[index - (maxCounterNumber + 1)]);
            }

            var vendingMachine = VendingMachine.GetInstance();
            int noOfTokens = 6;
            vendingMachine.GenerateTokens('W', noOfTokens);
           
            //Act
            //Assign tokens to the counters.
            bank.AssignTokensToCounters(VendingMachine.TokenList.Keys.ToList());

            //Assert
            //We expect C1 counter queue must have 5 tokens and C2 counter must have 4 tokens
            var c1Counter = bank.Counters["C1"];
            Assert.Equal(2, c1Counter.CounterQueue.Count);

            var c2Counter = bank.Counters["C2"];
            Assert.Equal(2, c2Counter.CounterQueue.Count);

            var c3Counter = bank.Counters["C3"];
            Assert.Equal(2, c3Counter.CounterQueue.Count);

        }

        [Fact]
        public void Evenly_Distribute_OddnumberOfTokens_To_OddnumberOfCounters_Test()
        {
            //Arrange
            //create odd no. of tokens
            VendingMachine.TokenList.Clear();
            ITokenDistributor tokenDistributor = new UniformTokenDistributor();
            IBank bank = new Bank(tokenDistributor);
            //create odd no. of counters
            var counters = SetupWithdrawCounters(10);

            //Act
            int maxCounterNumber = bank.Counters.Count;

            for (int index = maxCounterNumber + 1; index <= counters.Count; index++)
            {
                bank.Counters.Add('C' + index.ToString(), counters[index - (maxCounterNumber + 1)]);
            }
            
            var vendingMachine = VendingMachine.GetInstance();
            int noOfTokens = 9;
            vendingMachine.GenerateTokens('W', noOfTokens);
            
            //Act
            //Assign tokens to the counters.
            bank.AssignTokensToCounters(VendingMachine.TokenList.Keys.ToList());

            //Assert
            //We expect C1 counter queue must have 5 tokens and C2 counter must have 4 tokens
            var c1Counter = bank.Counters["C1"];
            Assert.Equal(3, c1Counter.CounterQueue.Count);

            var c2Counter = bank.Counters["C2"];
            Assert.Equal(3, c2Counter.CounterQueue.Count);

            var c3Counter = bank.Counters["C3"];
            Assert.Equal(3, c3Counter.CounterQueue.Count);
        }
    }
}