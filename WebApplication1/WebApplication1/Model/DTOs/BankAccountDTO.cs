using BankAcountService;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace BankAccountService.Model.DTOs;

// Example of Json: {"Id":1,"Balance":10.0,"Currency":"USD"}

public class BankAccountDTO
{
    public BankAccountDTO(BankAccountRecord bankAccount)
    {
        Id = bankAccount.Id;
        Balance = bankAccount.BankAccount.Balance.Amount;
        Currency = bankAccount.BankAccount.Balance.Currency;
    }

    public BankAccountDTO() { }

    public int Id { get; set; }
    public decimal Balance { get; set; }
    
    [JsonConverter(typeof(StringEnumConverter))]
    public Currency Currency { get; set; }

    public BankAccountRecord ToBankAccountRecord() =>
        new BankAccountRecord(Id, 
            new BankAccount(new(Balance, Currency)));

    public string ConvertToJson() => JsonConvert.SerializeObject(this);

    public static BankAccountDTO ConvertFromJson([StringSyntax(StringSyntaxAttribute.Json)] string bankAccountDto) =>
        JsonConvert.DeserializeObject<BankAccountDTO>(bankAccountDto) ?? 
            throw new InvalidOperationException("An error during deserialization has occured");

}
