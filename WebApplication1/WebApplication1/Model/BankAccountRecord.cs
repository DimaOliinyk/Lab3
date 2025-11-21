using BankAcountService;

namespace BankAccountService.Model;

/// <summary>
/// Represents an immutable bank account with id
/// </summary>
public class BankAccountRecord
{
    public int Id { private init; get; }
    public BankAccount BankAccount { get; private init; }

    public BankAccountRecord(int id, BankAccount bankAccount)
    {
        Id = id;
        BankAccount = bankAccount;
    }
}
