using BankAccountService.Model;
using BankAccountService.Model.DTOs;
using BankAcountService;
using QuestDB;

namespace BankAccountService.Database;

public class QuestDBConnection : ITransactionSaver
{
    private static string _connectionString = 
        AppSettings.Configuration.GetConnectionString("QuestDBConnection")!;

    private QuestDB.Senders.ISender _sender = 
        Sender.New(_connectionString);

    private Func<Currency, string> _convertEnumToString;

    public QuestDBConnection(Func<Currency, string>? currencyToStrConverter = null)
    {
        var defaultConverter = (Currency c) => Enum.GetName(typeof(Currency), c)!;
        _convertEnumToString = currencyToStrConverter ?? defaultConverter;
    }

    public void SaveInterestAccrual(int id, decimal procentage)
    {
        _sender.Table("InetersetAccruals")
               .Column("AccountId", id)
               .Column("Procentage", procentage)
               .At(DateTime.UtcNow);
        _sender.Send();
    }

    public void SaveReplenish(int id, decimal amount, Currency currency)
    {
        _sender.Table("Replenishments")
               .Column("AccountId", id)
               .Column("Amount", amount)
               .Column("Currency", _convertEnumToString(currency))
               .At(DateTime.UtcNow);
        _sender.Send();
    }

    public void SaveWithdraw(int id, decimal amount, Currency currency)
    {
        _sender.Table("Withdrawls")
               .Column("AccountId", id)
               .Column("Amount", amount)
               .Column("Currency", _convertEnumToString(currency))
               .At(DateTime.UtcNow);
        _sender.Send();
    }
}
