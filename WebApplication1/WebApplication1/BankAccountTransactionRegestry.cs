using BankAccountService.Model;
using BankAccountService.Model.DTOs;
using BankAcountService;

namespace BankAccountService;

using TransactioActions = Action<BankAccountRecord, Money>;

public static class BankAccountTransactionRegestry
{
    private static Dictionary<string, TransactioActions> _transactions = new();

    public static void RegisterTransaction(string transactionName, TransactioActions transaction) =>
        _transactions.Add(transactionName, transaction);

    public static void CallTransacion(string transactionName, BankAccountRecord record, Money transactionData) =>
        _transactions[transactionName]?.Invoke(record, transactionData);
}
