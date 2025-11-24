using BankAccountService.Model;
using BankAccountService.Model.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BankAcountService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        var bn = new BankAccountRecord(1, new BankAccount(new(10, Currency.USD)));
        var dto = new BankAccountDTO(bn);

        app.MapGet("/", () => "Bank account service is up");

        app.Run();

        // TODO: Create BankAccountController to covert json body to BankAccountRecord

        app.MapPost("api/v1/{function}", (string function, [FromBody] string jsonAccount) =>
        {
            var bankAccRecord = BankAccountDTO.ConvertFromJson(jsonAccount);

            switch (function)
            {
                case "replenish":
                    throw new NotImplementedException();
                case "withdraw":
                    throw new NotImplementedException();
                case "interestaccrual":
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            };
            // And save to time db
        });
    }
}
