using BankOperations.Types;

namespace BankOperations.Abstracts
{
    /// <summary>
    /// Representation of a token
    /// </summary>
    public abstract class Token
    {
        /// <summary>
        /// Is the token processing done
        /// </summary>
        public bool IsProcessed { get; set; }

        /// <summary>
        /// User friendly name of the token, such as D1, W2, O3
        /// </summary>
        public string TokenId { get; set; }

        /// <summary>
        /// Represents type of transaction (monetory\non monetory)
        /// </summary>
        private TransactionType TxnType { get; set; }

        /// <summary>
        /// Type of the token (deposit\withdraw\other)
        /// </summary>
        public TokenType Type;


        protected Token(TransactionType type)
        {
            TxnType = type;
        }
    }
}
