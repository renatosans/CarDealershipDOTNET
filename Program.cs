var builder = WebApplication.CreateBuilder(args);

// Inject Swagger Services 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { });

var app = builder.Build();

//Use Swagger in application. 
app.UseSwagger();
app.UseSwaggerUI();


// Sample Endpoint
app.MapGet("/", () => "Hello! This is .NET 6 Minimal API.   /swagger to get Endpoints doc.");

app.Run();
