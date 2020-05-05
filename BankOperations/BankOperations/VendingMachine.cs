using BankOperations.Abstracts;
using BankOperations.Implementations;
using BankOperations.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankOperations
{

    // This Singleton implementation is called "double check lock". It is safe
    // in multi threaded environment and provides lazy initialization for the
    // Singleton object.

    /// <summary>
    /// Single vending machine of the bank for generating tokens
    /// </summary>
    public class VendingMachine
    {
            //TODO: Read the following from a config file.
            private const string TimeToResetTokenNumbers = "12:00 AM";

            private VendingMachine() { }

            private static VendingMachine _instance;

            public static Dictionary<Token, int> TokenList = new Dictionary<Token,int>();
           
            private static readonly object Lock = new object();

            public static VendingMachine GetInstance()
            {
                // This conditional is needed to prevent threads stumbling over the
                // lock once the instance is ready.
                if (_instance == null)
                {
                    
                    lock (Lock)
                    {
                        _instance = _instance ?? new VendingMachine();
                    }
                }
                return _instance;
            }

        public void GenerateTokens(char tokenType,int howManyTokens)
        {
            var locker = new object();
            lock (locker)
            {
                //At 12:00 AM, clear all the tokens, so that they are reset to 1.
                if (DateTime.Now.ToShortTimeString() == TimeToResetTokenNumbers)
                {
                    TokenList.Clear();
                }

                int lastTokenIndex = TokenList.Where(thisPair => thisPair.Key.TokenId[0] == tokenType).Select(thisPair => thisPair.Value).DefaultIfEmpty().Max();
                for (int index = lastTokenIndex + 1; index <= (lastTokenIndex + howManyTokens); index++)
                {
                    var currentToken = CreateToken(tokenType, index);
                    //TokenList dictionary is accessed by multiple requests to keep their tokens, hence making it threadsafe.
               
                    TokenList.Add(currentToken, index);
                }
            }
           
        }

        private static Token CreateToken(char operation, int counter)
        {
            Token currentToken;
            switch (operation)
            {
                case 'D':
                    currentToken = new MonetoryTxnToken(TokenType.Deposit, counter);
                    break;
                case 'W':
                    currentToken = new MonetoryTxnToken(TokenType.Withdraw, counter);
                    break;
                default:
                    currentToken = new NonMonetoryTxnToken(TokenType.Other, counter);
                    break;
            }

            return currentToken;
        }
    }
}
