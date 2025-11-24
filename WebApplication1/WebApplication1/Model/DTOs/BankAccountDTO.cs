using BankAcountService;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace BankAccountService.Model.DTOs;

// Example of Json: {"Id":1,"Balance":10.0,"Currency":"USD","RequestAmount":null,"RequestCurrency":null}
// Example of Json with request: {"Id":1,"Balance":10.0,"Currency":"USD","RequestAmount":5.0,"RequestCurrency":0}

public record RequestValues(decimal Amount, Currency Currency);

public class BankAccountDTO
{
    public BankAccountDTO(BankAccountRecord bankAccount, RequestValues? requestValues = null)
    {
        Id = bankAccount.Id;
        Balance = bankAccount.BankAccount.Balance.Amount;
        Currency = bankAccount.BankAccount.Balance.Currency;

        RequestCurrency = (requestValues != null) ? requestValues.Currency : null;
        RequestAmount = (requestValues != null) ? requestValues.Amount : null;
    }

    public BankAccountDTO() { }

    public int Id { get; set; }
    public decimal Balance { get; set; }
    
    [JsonConverter(typeof(StringEnumConverter))]
    public Currency Currency { get; set; }

    public decimal? RequestAmount { get; set; }
    public Currency? RequestCurrency { get; set; }

    public BankAccountRecord ToBankAccountRecord() =>
        new BankAccountRecord(Id, 
            new BankAccount(new(Balance, Currency)));

    public string ConvertToJson() => JsonConvert.SerializeObject(this);

    public static BankAccountDTO ConvertFromJson([StringSyntax(StringSyntaxAttribute.Json)] string bankAccountDto) =>
        JsonConvert.DeserializeObject<BankAccountDTO>(bankAccountDto) ?? 
            throw new InvalidOperationException("An error during deserialization has occured");

}
