using System;
using BankOperations.Abstracts;
using BankOperations.Types;

namespace BankOperations.Implementations
{
    /// <summary>
    /// Non Monetory transaction type of token (others like update passbook, open account etc..)
    /// </summary>
    public class NonMonetoryTxnToken : Token
    {
        public NonMonetoryTxnToken(TokenType type, int counter) : base(TransactionType.NonMonetory)
        {
            Type = type;
            TokenId = (Enum.GetName(typeof(TokenType), (int)type)[0]) + counter.ToString();
            IsProcessed = false;
        }
    }
}
