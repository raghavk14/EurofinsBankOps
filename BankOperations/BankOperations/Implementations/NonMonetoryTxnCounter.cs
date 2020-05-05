using BankOperations.Abstracts;
using BankOperations.Types;

namespace BankOperations.Implementations
{
    /// <summary>
    /// Counter which entertains only non-monetory tokens like (open account\update passbook)
    /// </summary>
    public class NonMonetoryTxnCounter : Counter
    {
      
        public NonMonetoryTxnCounter(TokenType tokenType,int capacity):base(TransactionType.NonMonetory,tokenType,capacity)
        {
           
        }
    }
}
