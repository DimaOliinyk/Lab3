using BankAccountService.Model;
using BankAccountService.Model.DTOs;
using BankAcountService;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine.ClientProtocol;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace BankAccountDTO.Tests;

public class BankAccountDTOTests
{
    private static string _payload = """
    {
        "Id":1,
        "Balance":10.0,
        "Currency":"USD"
    }        
    """;

    [Fact]
    public void ConvertsionIsCorrect()
    {
        var actual = BankAccountService.Model.DTOs.BankAccountDTO.ConvertFromJson(_payload);
        var excpected = new BankAccountService.Model.DTOs.BankAccountDTO(new BankAccountRecord(
            1, new BankAccount(
                new Money(10.0m, Currency.USD))));
        Assert.Equal(actual.Id, excpected.Id);
        Assert.Equal(actual.Balance, excpected.Balance);
        Assert.Equal(actual.Currency, excpected.Currency);
    }
}
