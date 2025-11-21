using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.tests
{
    /// <summary>
    /// Represents an immutable, decimal, non-negative amount of given currency
    /// </summary>

    public class Money
    {
        private decimal _amount;

        /// <exception cref="ArgumentException"></exception>
        public decimal Amount
        {
            get => _amount;
            private init => _amount = (value < 0) ? 
                throw new ArgumentException("Amount can't be negative") : 
                value;
        }

        public Currency Currency { get; private init; }


        /// <exception cref="ArgumentException"></exception>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        public Money(decimal amount, Currency currency) 
        { 
            Amount = amount;
            Currency = currency;
        }     
    }
}
