using System;
using System.Collections.Generic;
using System.Threading;
using BankOperations.Types;

namespace BankOperations.Abstracts
{
    /// <summary>
    /// Representation of Bank Counter
    /// </summary>
    public abstract class Counter
    {
        TransactionType TransactionType { get; }

        public TokenType TokenType { get; set; }

        public Queue<Token> CounterQueue { get; set; }

        protected Counter(TransactionType txnType, TokenType tokenType, int capacity)
        {
            if(capacity < 1)
                throw new InvalidOperationException("Incorrect counter capacity");

            TransactionType = txnType;
            TokenType = tokenType;
            CounterQueue = new Queue<Token>(capacity);
        }

        /// <summary>
        /// Represents the processing of a token at the counter
        /// </summary>
        public void ProcessTokens()
        {
            var thisToken = CounterQueue.Peek();
            //simulate operational delay
            Thread.Sleep(300);
            thisToken.IsProcessed = true;
            CounterQueue.Dequeue();
        }
    }
}
