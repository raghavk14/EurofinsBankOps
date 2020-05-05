using System.Collections.Generic;

namespace BankOperations.Abstracts
{
    /// <summary>
    /// Representation of Bank
    /// </summary>
    public interface IBank
    {
        /// <summary>
        /// Counters of the bank
        /// </summary>
        Dictionary<string, Counter> Counters { get; set; }

        /// <summary>
        /// Operation to distribute tokens across the available counters
        /// </summary>
        /// <param name="tokenList"></param>
        void AssignTokensToCounters(IList<Token> tokenList);

        /// <summary>
        /// Returns a collection of Counters and the next token to be processed in it
        /// </summary>
        /// <returns></returns>
        List<(string, string)> GetTokensGetDisplay();
    }
}
