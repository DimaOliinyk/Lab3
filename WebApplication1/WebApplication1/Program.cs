using BankAccountService.Model;

namespace BankAcountService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Bank account service is up");

        app.Run();

        // TODO: Create BankAccountController to covert json body to BankAccountRecord

        app.MapPost("api/v1/withdraw", () => 
        {
            // TODO: withdraw from account
            // And save to time db
        });
        app.MapPost("api/v1/replenish", () => 
        {
            // TODO: replenish account
            // And save to time db
        });
        app.MapPost("api/v1/interestaccrual", () => 
        {
            // TODO: replenish account
            // And save to time db
        });
    }
}
