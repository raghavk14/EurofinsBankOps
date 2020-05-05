using System;
using System.Collections.Generic;
using System.Text;

namespace BankOperations.Abstracts
{
    /// <summary>
    /// Represents the entity responsible for distributing the tokens
    /// </summary>
    public interface ITokenDistributor
    {
        void SegregateTokensAndCounters(IList<Token> tokenList, Dictionary<string, Counter> counters);

    }
}
