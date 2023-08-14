using TestDarkThread;

var config = new Config();

int userNo = 1;
while (true) 
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Tokens: " + string.Join(", ", config.ListUsers()));
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write(@"1.Set ClientId
2.Add AccessToken
3.Refresh
Option: ");
    string opt = Console.ReadLine() ?? "?";
    switch (opt) {
        case "1":
            Console.Write("Client Id: ");
            var clientId = Console.ReadLine();
            config.SetAuthIdentity(clientId!, "NA");
            break;
        case "2":
            config.SaveAccessToken($"User{userNo++}", "");
            Task.Delay(500).Wait(); // Waiting for reloadOnChange
            break;
        case "3":
            break;
        default:
            Console.WriteLine($"Unknown command - {opt}") ;
            break;
    }
}

// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

// app.Run();
