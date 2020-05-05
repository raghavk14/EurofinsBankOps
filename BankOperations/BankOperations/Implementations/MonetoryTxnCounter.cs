using BankOperations.Abstracts;
using BankOperations.Types;

namespace BankOperations.Implementations
{
    /// <summary>
    /// Counter which entertains only monetory tokens like (deposit\withdraw)
    /// </summary>
    public class MonetoryTxnCounter : Counter
    {
       
        public MonetoryTxnCounter(TokenType tokenType,int capacity) : base(TransactionType.Monetory,tokenType,capacity)
        {
            //specific operations and validations go here.
        }
    }
}
