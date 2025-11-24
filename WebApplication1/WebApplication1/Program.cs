using BankAccountService.Model;
using BankAccountService.Model.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        app.MapPut("/api/v1/{function}", async (string function, HttpRequest request) =>
        {
            using var sr = new StreamReader(request.Body, System.Text.Encoding.UTF8);
            var rawBody = await sr.ReadToEndAsync();
            Debug.WriteLine(rawBody);

            var dto = BankAccountDTO.ConvertFromJson(rawBody);
            var rec = dto.ToBankAccountRecord();
            var bankAcc = rec.BankAccount;

            if (dto.RequestAmount is null || dto.RequestCurrency is null) 
            {
                return Results.Problem("Parameter was not specified");
            }

            var reqMoney = new Money((decimal)dto.RequestAmount, (Currency)dto.RequestCurrency);

            switch (function)
            { 
                case "replenish":
                    bankAcc.Replenish(reqMoney);
                    break;
                case "withdraw":
                    bankAcc.Withdraw(reqMoney);
                    break;
                case "interestaccrual":
                    bankAcc.InterestAccrual(reqMoney.Amount);
                    break;
                default:
                    return Results.Problem("An unexpected error occurred.");
            };

            var resultDto = new BankAccountDTO(rec);
            return Results.Ok(resultDto.ConvertToJson());
            // TODO: And save to time db
        });

        app.Run();
    }
}
