var builder = WebApplication.CreateBuilder(args);

// Inject Connection String and Create EFCore DB Context 
builder.Services.AddDbContext<CarDealershipDB>(options => {
    String connectionString = builder.Configuration["ConnectionStrings:Default"];
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Inject Swagger Services 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { });

var app = builder.Build();

//Use Swagger in application. 
app.UseSwagger();
app.UseSwaggerUI();


// Sample Endpoint
app.MapGet("/", () => "Hello! This is .NET 6 Minimal API.   /swagger to get Endpoints doc.");

// Get all cars from database
app.MapGet("/cars", async (CarDealershipDB db) => await db.Cars.ToListAsync())
.Produces<List<CarsForSale>>(StatusCodes.Status200OK)
.WithName("GetAllCars").WithTags("Getters");


// Add a new Car to database
app.MapPost("/cars", async ([FromBody] CarsForSale newCar, [FromServices] CarDealershipDB db, HttpResponse response) => {
    db.Cars.Add(newCar);
    await db.SaveChangesAsync();
    response.StatusCode = 200;
    response.Headers.Location = $"cars/{newCar.id}";
})
.Accepts<CarsForSale>("application/json")
.Produces<CarsForSale>(StatusCodes.Status201Created)
.WithName("AddNewCar").WithTags("Setters");


// Get Cars from database filtered by price
app.MapGet("/cars_by_price", async (decimal min, decimal max, CarDealershipDB db) =>
    await db.Cars.Where(vehicle => vehicle.price >= min && vehicle.price <= max).ToListAsync()
)
.Produces<List<CarsForSale>>(StatusCodes.Status200OK)
.WithName("GetCarsByPrice").WithTags("Getters");


// Get Cars from database filtered
app.MapGet("/cars_filtered", async (int fromYear, int toYear, int fromMileage, int toMileage, decimal fromPrice, decimal toPrice, CarDealershipDB db) =>
    await db.Cars
        .Where(vehicle => vehicle.year >= fromYear && vehicle.year <= toYear)
        .Where(vehicle => vehicle.mileage >= fromMileage && vehicle.mileage <= toMileage)
        .Where(vehicle => vehicle.price >= fromPrice && vehicle.price <= toPrice)
        .ToListAsync()
)
.Produces<List<CarsForSale>>(StatusCodes.Status200OK)
.WithName("GetCarsFiltered").WithTags("Getters");


app.Run();
