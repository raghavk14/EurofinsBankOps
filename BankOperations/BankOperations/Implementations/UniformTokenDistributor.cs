using BankOperations.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using BankOperations.Types;

namespace BankOperations.Implementations
{

    /// <summary>
    /// Uniformly distributes tokens among the available counters in RR fashion
    /// </summary>
    public class UniformTokenDistributor : ITokenDistributor
    {
        public void SegregateTokensAndCounters(IList<Token> tokenList, Dictionary<string, Counter> counters)
        {
            //Make the groups out of tokens based on token type.
            var depositTokens = tokenList.Where(token => token.Type == TokenType.Deposit).Select(token => token).ToList();
            var withdrawTokens = tokenList.Where(token => token.Type == TokenType.Withdraw).Select(token => token).ToList();
            var otherTokens = tokenList.Where(token => token.Type == TokenType.Other).Select(token => token).ToList();
            
            if(depositTokens.Any())
                AssignTokensToCounters(depositTokens, TokenType.Deposit, counters);

            if(withdrawTokens.Any())
                AssignTokensToCounters(withdrawTokens, TokenType.Withdraw, counters);

            if(otherTokens.Any())
                AssignTokensToCounters(otherTokens, TokenType.Other, counters);
        }

        private void AssignTokensToCounters(IEnumerable<Token> classifiedTokens,TokenType tokenType, Dictionary<string, Counter> counters)
        {
            var tokenBasedCounters = counters.Where(counter => counter.Value.TokenType == tokenType).Select(counter => counter).ToList();

            int count = tokenBasedCounters.Count();
            bool doesTokensExist = classifiedTokens.Any();

            if (doesTokensExist && count == 0)
                throw new InvalidOperationException($"No appropriate counter available to process {tokenType} tokens");

            if (count == 0 || !doesTokensExist)
                return;
            
            int counterIndex = 0;

            foreach (var token in classifiedTokens)
            {
                tokenBasedCounters[counterIndex].Value.CounterQueue.Enqueue(token);
                counterIndex++;
                if (counterIndex > count - 1)
                    counterIndex = 0;
            }

        }
    }
}
