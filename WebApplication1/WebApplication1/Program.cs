namespace BankAcountService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");

        app.Run();

        var a = new BankAccount(new Money(10, Currency.USD));
    }
}
