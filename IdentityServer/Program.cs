var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly.GetName().Name;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// builder.Services.AddIdentity<>










var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
