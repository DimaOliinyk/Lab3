using BankAccountService.Model.DTOs;
using BankAcountService;

namespace BankAccountService.Model;

public interface ITransactionSaver
{
    void SaveWithdraw(int id, decimal amount, Currency currency);
    void SaveReplenish(int id, decimal amount, Currency currency);
    void SaveInterestAccrual(int id, decimal procentage);
}