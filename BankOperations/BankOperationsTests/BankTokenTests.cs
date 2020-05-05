using System;
using Xunit;
using BankOperations;
using System.Collections.Generic;
using System.Linq;
using BankOperations.Abstracts;
using BankOperations.Implementations;
using BankOperations.Types;

namespace BankOperationsTests
{
    public class BankTokenTests
    {
        private IList<Counter> SetupBankCounters(int capacity)
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

        [Fact]
        public void Constructor_Test()
        {
            //Arrange
            ITokenDistributor tokenDistributor = new UniformTokenDistributor();
            IBank bank = new Bank(tokenDistributor);
            var counters = SetupBankCounters(10);
            //Act
            int maxCounterNumber = bank.Counters.Count;

            for (int index = maxCounterNumber + 1; index <= counters.Count; index++)
            {
                bank.Counters.Add('C' + index.ToString(), counters[index - (maxCounterNumber + 1)]);
            }

            //Assert
            Assert.NotNull(bank);
            Assert.IsAssignableFrom<IBank>(bank);
        }

        [Fact]
        public void Generate_Deposit_Token_Test()
        {
            VendingMachine.TokenList.Clear();

            int depositTokenCounter = 1;
            var vendingMachine = VendingMachine.GetInstance();
            vendingMachine.GenerateTokens('D',depositTokenCounter);
           
            Assert.IsAssignableFrom<VendingMachine>(vendingMachine);
            Assert.NotNull(vendingMachine);
            Assert.NotNull(VendingMachine.TokenList.Keys.ToList()[0]);
            Assert.IsAssignableFrom<MonetoryTxnToken>(VendingMachine.TokenList.Keys.ToList()[0]);
            Assert.Equal("D1", VendingMachine.TokenList.Keys.ToList()[0].TokenId);
        }

        [Fact]
        public void Generate_Multiple_Withdraw_Tokens_Test()
        {
            VendingMachine.TokenList.Clear();
            
            var vendingMachine = VendingMachine.GetInstance();
            int noOfTokens = 5;
            vendingMachine.GenerateTokens('W', noOfTokens);
            
            Assert.Equal(noOfTokens, VendingMachine.TokenList.Keys.Select(thisKey => thisKey.TokenId[0] == 'W').Count());
        }

        [Fact]
        public void Generate_Tokens_With_Serial_Numbers_Test()
        {
            VendingMachine.TokenList.Clear();

            int depositTokenCounter = 1;
            var vendingMachine = VendingMachine.GetInstance();

            vendingMachine.GenerateTokens('D', depositTokenCounter);

            Assert.NotNull(vendingMachine);
            Assert.IsAssignableFrom<VendingMachine>(vendingMachine);
            Assert.NotNull(VendingMachine.TokenList.Keys.ToList()[0]);
            Assert.IsAssignableFrom<MonetoryTxnToken>(VendingMachine.TokenList.Keys.ToList()[0]);
            Assert.Equal("D1", VendingMachine.TokenList.Keys.ToList()[0].TokenId);

            //Get next Depositor token from the vending machine.
            //int lastDepositTokenNumber = VendingMachine.TokenList.Where(thisPair => thisPair.Key.TokenId[0] == 'D').Select(thisPair => thisPair.Value).Max();

            //D2 Token
            vendingMachine.GenerateTokens('D', depositTokenCounter);
            Assert.NotNull(vendingMachine);
            Assert.IsAssignableFrom<VendingMachine>(vendingMachine);
            var nextToken = VendingMachine.TokenList.Keys.ToList()[1];
            Assert.NotNull(nextToken);
            Assert.IsAssignableFrom<MonetoryTxnToken>(nextToken);
            Assert.Equal("D2", nextToken.TokenId);

            //lastDepositTokenNumber = VendingMachine.TokenList.Where(thisPair => thisPair.Key.TokenId[0] == 'D').Select(thisPair => thisPair.Value).Max();

            //D3 Token
            vendingMachine.GenerateTokens('D', depositTokenCounter);
            Assert.NotNull(vendingMachine);
            Assert.IsAssignableFrom<VendingMachine>(vendingMachine);
            nextToken = VendingMachine.TokenList.Keys.ToList()[2];
            Assert.NotNull(nextToken);
            Assert.IsAssignableFrom<MonetoryTxnToken>(nextToken);
            Assert.Equal("D3", nextToken.TokenId);
        }
    }
}
