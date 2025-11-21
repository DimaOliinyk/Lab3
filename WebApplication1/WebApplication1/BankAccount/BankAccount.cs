using System.ComponentModel;

namespace BankAccount.tests;


/// <summary>
/// Represents a balance of personal bank account 
/// </summary>
public class BankAccount
{
    public Money Balance { get; private set; }

    public BankAccount(Money initialBalance)
    {
        Balance = initialBalance;
    }

    /// <summary>
    /// Withdraws money from balance <br/>
    /// Throws exception if wrong currency is used or balance has insufficient funds
    /// </summary>
    /// <param name="amount"></param>
    /// <exception cref="InvalidOperationException"></exception>

    public void Withdraw(Money amount)
    {
        if (Balance.Amount < amount.Amount)
        {
            throw new InvalidOperationException("Insufficient funds");
        } 

        if (amount.Currency != Balance.Currency)
        {
            throw new InvalidEnumArgumentException("Wrong currency");
        }
        Balance = new Money(Balance.Amount - amount.Amount, Balance.Currency);
    }

    /// <summary>
    /// Replenishes balance by a given amount <br/>
    /// Throws exception if different currencies are used
    /// </summary>
    /// <param name="amount"></param>
    /// <exception cref="InvalidEnumArgumentException"></exception>
    public void Replenish(Money amount) =>
        Balance = (Balance.Currency == amount.Currency) switch
        {
            true => new(amount.Amount + Balance.Amount, Balance.Currency),
            _ => throw new InvalidEnumArgumentException("Wrong currency")
        };

    /// <summary>
    /// Increases balance by specified interest rate <br/>
    /// Throws exception if non-positive interest rate is used 
    /// </summary>
    /// <param name="interestRate"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void InterestAccrual(decimal interestRate) =>
        Balance = (interestRate > 0) switch
        {
            true => new(Balance.Amount + Balance.Amount * interestRate, Balance.Currency),
            _ => throw new ArgumentException("Interest rate cannot be non-positive")
        };
}
