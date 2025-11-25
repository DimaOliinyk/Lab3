using BankAccountService;
using BankAccountService.Database;
using BankAccountService.Model;
using BankAccountService.Model.DTOs;
using System.Diagnostics;

namespace BankAcountService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        ITransactionSaver saver = new QuestDBConnection();

        BankAccountTransactionRegestry.RegisterTransaction(
            "replenish", 
            (BankAccountRecord bankAccRec, Money m) =>
            {
                bankAccRec.BankAccount.Replenish(m);
                saver.SaveReplenish(bankAccRec.Id, m.Amount, m.Currency);
            });
        BankAccountTransactionRegestry.RegisterTransaction(
            "withdraw",
            (BankAccountRecord bankAccRec, Money m) =>
            {
                bankAccRec.BankAccount.Withdraw(m);
                saver.SaveWithdraw(bankAccRec.Id, m.Amount, m.Currency);
            });
        BankAccountTransactionRegestry.RegisterTransaction(
            "interestaccrual",
            (BankAccountRecord bankAccRec, Money m) =>
            {
                bankAccRec.BankAccount.InterestAccrual(m.Amount);
                saver.SaveInterestAccrual(bankAccRec.Id, m.Amount);
            });

        app.MapGet("/", () => "Bank account service is up");

        app.MapPut("/api/v1/{action}", async (string action, HttpRequest request) =>
        {
            using var sr = new StreamReader(request.Body, System.Text.Encoding.UTF8);

            var dto = BankAccountDTO.ConvertFromJson(await sr.ReadToEndAsync());
            var rec = dto.ToBankAccountRecord();

            if (dto.RequestAmount is null || dto.RequestCurrency is null)
                return Results.Problem("Parameter was not specified");

            Money reqMoney = new(
                (decimal)dto.RequestAmount, 
                (Currency)dto.RequestCurrency);

            try
            {
                BankAccountTransactionRegestry.CallTransacion(action, rec, reqMoney);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Results.Problem("An unexpected error occurred.");
            }

            var resultDto = new BankAccountDTO(rec);
            return Results.Ok(resultDto.ConvertToJson());
            // TODO: And save to time db
        });

        app.Run();
    }
}
