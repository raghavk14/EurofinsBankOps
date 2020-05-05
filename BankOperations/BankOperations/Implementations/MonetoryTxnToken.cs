using System;
using BankOperations.Abstracts;
using BankOperations.Types;

namespace BankOperations.Implementations
{
    /// <summary>
    /// Monetory transaction type of token (deposit\withdraw)
    /// </summary>
    public class MonetoryTxnToken : Token
    {
        
        public MonetoryTxnToken(TokenType type, int counter) : base(TransactionType.Monetory)
        {
            Type = type;
            TokenId = (Enum.GetName(typeof(TokenType), (int)type)[0]) + counter.ToString();
            IsProcessed = false;
        }
    }
}
