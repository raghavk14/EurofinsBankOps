using System;
using System.Collections.Generic;
using System.Linq;
using BankOperations.Abstracts;

namespace BankOperations.Implementations
{
    /// <summary>
    /// Implementation of Bank
    /// </summary>
    public class Bank : IBank
    {
        /// <summary>
        /// Counters defined in the bank
        /// </summary>
        public Dictionary<string, Counter> Counters { get; set; }

        /// <summary>
        /// Placeholder for the type of token distributor algorithm
        /// </summary>
        private readonly ITokenDistributor _tokenDistributor;

        /// <summary>
        /// Constructor with dependency injection of token distributor
        /// </summary>
        /// <param name="tokenDistributor"></param>
        public Bank(ITokenDistributor tokenDistributor)
        {
            Counters = new Dictionary<string, Counter>();
            _tokenDistributor = tokenDistributor;
        }

        
        /// <summary>
        /// Method to allot tokens to the counters
        /// </summary>
        /// <param name="tokenList"></param>
        public void AssignTokensToCounters(IList<Token> tokenList)
        {
            _tokenDistributor.SegregateTokensAndCounters(tokenList,this.Counters);
           
        }

        /// <summary>
        /// Returns a collection of Counters and the next token to be processed in it
        /// </summary>
        /// <returns>A tuple representing counter name and token at its front</returns>
        public List<(string, string)> GetTokensGetDisplay()
        {
            //Iterate through the counters, and show the tokens at exists at their front.
            var tokenDashboard = new List<(string,string)>();
            foreach (var counter in Counters.Where(thisCounter => thisCounter.Value.CounterQueue.Any()))
            {
                tokenDashboard.Add((counter.Key,counter.Value.CounterQueue.Peek().TokenId));
            }

            return tokenDashboard;
        }

       
    }
}
