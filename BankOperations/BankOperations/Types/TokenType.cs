using System;

namespace BankOperations.Types
{
    /// <summary>
    /// Type of the token
    /// </summary>
    [Flags]
    public enum TokenType
    {
        Invalid = 0,
        Deposit = 1,
        Withdraw = 2,
        Other = 3
    }
}