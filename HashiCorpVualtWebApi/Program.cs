using VaultSharp.V1.AuthMethods.Token;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.SecretsEngines;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure VaultSharp
builder.Services.AddSingleton<IVaultClient>(serviceProvider =>
{
    
    var vaultClientSettings = new VaultClientSettings("http://127.0.0.1:8200", new TokenAuthMethodInfo("you hashicorp vualt token"))
    {
        SecretsEngineMountPoints = new SecretsEngineMountPoints
        {
            KeyValueV2 = "kv" // Use the correct mount point where your KV v2 is mounted
        }
    };

    return new VaultClient(vaultClientSettings);
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

