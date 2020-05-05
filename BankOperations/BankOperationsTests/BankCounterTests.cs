using BankOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using BankOperations.Abstracts;
using BankOperations.Implementations;
using BankOperations.Types;
using Xunit;

namespace BankOperationsTests
{
    public class BankCounterTests
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
        public void InValid_Counter_Capacity_Throws_Exception_Test()
        {
            Action act = () => SetupBankCounters(0);
            //assert
            var exception = Assert.Throws<InvalidOperationException>(act);

            Assert.Equal("Incorrect counter capacity", exception.Message);
        }

        [Fact]
        public void Display_Bank_Counters_Test()
        {
            //Arrange
            var expectedCounterIdList = new List<string> {"C1", "C2", "C3", "C4", "C5"};
            ITokenDistributor tokenDistributor = new UniformTokenDistributor();
            IBank bank = new Bank(tokenDistributor);
            var counters = SetupBankCounters(10);

            int maxCounterNumber = bank.Counters.Count;
            //Act
            for (int index = maxCounterNumber + 1; index <= counters.Count; index++)
            {
                bank.Counters.Add('C' + index.ToString(), counters[index - (maxCounterNumber + 1)]);
            }

            //Assert
            Assert.NotNull(bank);
            Assert.IsAssignableFrom<IBank>(bank);

            Assert.Equal(expectedCounterIdList, bank.Counters.Keys.ToList());

        }

        [Fact]
        public void GetTokensDisplay_Test()
        {
            //Arrange
            //First setup counters of the bank. (C1:Deposit, C2:Withdraw, C3:Withdraw, C4:Other, C5:Other)
            //Arrange
            var expectedDashboard = new List<(string, string)>
            {
                ("C1", "D1"),
                ("C2", "W1"),
                ("C3", "W2"),
                ("C4", "O1"),
                ("C5", "O2")
            };

            ITokenDistributor tokenDistributor = new UniformTokenDistributor();
            IBank bank = new Bank(tokenDistributor);
            var counters = SetupBankCounters(10);

            int maxCounterNumber = bank.Counters.Count;
           
            for (int index = maxCounterNumber + 1; index <= counters.Count; index++)
            {
                bank.Counters.Add('C' + index.ToString(), counters[index - (maxCounterNumber + 1)]);
            }

            //Assign 2 deposit tokens to each of these counters.
            VendingMachine.TokenList.Clear();

            int noOfDepositTokens = 2;
            var vendingMachine = VendingMachine.GetInstance();
            vendingMachine.GenerateTokens('D', noOfDepositTokens);

            //Assign deposit tokens to the counters.
            //bank.AssignTokensToCounters(VendingMachine.TokenList.Keys.ToList());

            //Assign 3 withdraw tokens to the counters.
            int noOfWithdrawTokens = 3;
            vendingMachine.GenerateTokens('W', noOfWithdrawTokens);
            
            //bank.AssignTokensToCounters(VendingMachine.TokenList.Keys.ToList());

            //Assign 3 withdraw tokens to the counters.
            int noOfOtherTokens = 4;
            vendingMachine.GenerateTokens('O', noOfOtherTokens);
            
            //Segregate and assign tokens to the appropriate counters.
            bank.AssignTokensToCounters(VendingMachine.TokenList.Keys.ToList());

            //Act
            var actualDashBoard = bank.GetTokensGetDisplay();

            //Assert
            Assert.NotNull(bank);
            Assert.NotNull(bank.Counters);
            Assert.Equal(expectedDashboard,actualDashBoard);
        }

        [Fact]
        public void Process_Tokens_Update_Display_Test()
        {
            //Arrange
            //First setup counters of the bank. (C1:Deposit, C2:Withdraw, C3:Withdraw, C4:Other, C5:Other)
            //Arrange
            var expectedDashboard = new List<(string, string)>
            {
                ("C1", "D1"),
                ("C2", "W1"),
                ("C3", "W2"),
                ("C4", "O1"),
                ("C5", "O2")
            };

            ITokenDistributor tokenDistributor = new UniformTokenDistributor();
            IBank bank = new Bank(tokenDistributor);
            var counters = SetupBankCounters(10);

            int maxCounterNumber = bank.Counters.Count;

            for (int index = maxCounterNumber + 1; index <= counters.Count; index++)
            {
                bank.Counters.Add('C' + index.ToString(), counters[index - (maxCounterNumber + 1)]);
            }

            //Assign 2 deposit tokens to each of these counters.
            VendingMachine.TokenList.Clear();
            
            var vendingMachine = VendingMachine.GetInstance();

            int noOfDepositTokens = 2;
            vendingMachine.GenerateTokens('D', noOfDepositTokens);
            

            //Assign deposit tokens to the counters.
            //bank.AssignTokensToCounters(VendingMachine.TokenList.Keys.ToList());

            //Assign 3 withdraw tokens to the counters.
           
            int noOfWithdrawTokens = 3;
            vendingMachine.GenerateTokens('W', noOfWithdrawTokens);

            //bank.AssignTokensToCounters(VendingMachine.TokenList.Keys.ToList());

            //Assign 3 withdraw tokens to the counters.
           
            int noOfOtherTokens = 4;
            vendingMachine.GenerateTokens('O', noOfOtherTokens);
            

            //Segregate and assign tokens to the appropriate counters.
            bank.AssignTokensToCounters(VendingMachine.TokenList.Keys.ToList());

            //Act
            var actualDashBoard = bank.GetTokensGetDisplay();

            //Assert
            Assert.NotNull(bank);
            Assert.NotNull(bank.Counters);
            Assert.Equal(expectedDashboard, actualDashBoard);

            //Lets process 1 deposit and 1 other token
            var expectedUpdatedDashboard = new List<(string, string)>
            {
                ("C1", "D2"),
                ("C2", "W1"),
                ("C3", "W2"),
                ("C4", "O3"),
                ("C5", "O2")
            };

            //Get the C1 counter and process it's token
            var c1Counter = bank.Counters.Where(dCounter => dCounter.Key == "C1").Select(dCounter => dCounter.Value).SingleOrDefault();
            c1Counter.ProcessTokens();

            var c4Counter = bank.Counters.Where(dCounter => dCounter.Key == "C4").Select(dCounter => dCounter.Value).SingleOrDefault();
            c4Counter.ProcessTokens();

            //Act
            var latestActualDashBoard = bank.GetTokensGetDisplay();

            //Assert
            Assert.Equal(expectedUpdatedDashboard, latestActualDashBoard);
        }

        [Fact]
        public void Assign_Invalid_Token_To_Counter_Throws_Exception_Test()
        {
            ITokenDistributor tokenDistributor = new UniformTokenDistributor();
            IBank bank = new Bank(tokenDistributor);
            
            //Create Non Monetory counters
            Counter nonMonetaryCounter1 = new NonMonetoryTxnCounter(TokenType.Other,10);
            Counter nonMonetaryCounter2 = new NonMonetoryTxnCounter(TokenType.Other,10);

            var counters = new List<Counter>
            {
                nonMonetaryCounter1,
                nonMonetaryCounter2
            };
            

            int maxCounterNumber = bank.Counters.Count;

            for (int index = maxCounterNumber + 1; index <= counters.Count; index++)
            {
                bank.Counters.Add('C' + index.ToString(), counters[index - (maxCounterNumber + 1)]);
            }

            //Assign 2 deposit tokens to each of these counters.
            VendingMachine.TokenList.Clear();
            
            var vendingMachine = VendingMachine.GetInstance();

            int noOfDepositTokens = 2;
            vendingMachine.GenerateTokens('D', noOfDepositTokens);
          
            var tokenType = VendingMachine.TokenList.Keys.ToList()[0].Type;
            //Act
            //Segregate and assign tokens to the appropriate counters.
            Action act = () => bank.AssignTokensToCounters(VendingMachine.TokenList.Keys.ToList());
            //assert
            var exception = Assert.Throws<InvalidOperationException>(act);
            //The thrown exception can be used for even more detailed assertions.
            
            Assert.Equal($"No appropriate counter available to process {tokenType} tokens", exception.Message);
        }
    }
}
