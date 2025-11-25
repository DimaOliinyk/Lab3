namespace BankAccountService;

public static class AppSettings
{
    public static IConfiguration Configuration { get; } = 
        new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .SetBasePath(Directory.GetCurrentDirectory())
            .Build();
}
