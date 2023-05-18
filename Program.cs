
var builder = WebApplication.CreateBuilder(args);

// Inject Connection String and Create EFCore DB Context 
builder.Services.AddDbContext<CarDealershipDB>(options => {
    String connectionString = builder.Configuration["ConnectionStrings:Default"];
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Enable CORS
builder.Services.AddCors();

// Inject Swagger Services 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { });

var app = builder.Build();
app.UsePathBase(new PathString("/api"));
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


//Use Swagger in application. 
app.UseSwagger();
app.UseSwaggerUI();


// Sample Endpoint
app.MapGet("/", () => "Hello! This is .NET 6 Minimal API.   /swagger to get Endpoints doc.").ExcludeFromDescription();

// Get all cars from database
app.MapGet("/cars", async (CarDealershipDB db) => await db.Cars.ToListAsync())
.Produces<List<CarsForSale>>(StatusCodes.Status200OK)
.WithName("GetAllCars").WithTags("Getters");


// Get all customers from database
app.MapGet("/customers", async (CarDealershipDB db) => await db.Customers.ToListAsync())
.Produces<List<Customer>>(StatusCodes.Status200OK)
.WithName("GetAllCustomers").WithTags("Getters");

// Add a new Car to database
app.MapPost("/cars", async ([FromBody] VehiclePayload payload, [FromServices] CarDealershipDB db, HttpResponse response) => {
    String outputDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\img";
    Directory.CreateDirectory(outputDir);

    String filename = "generate_new_file_name";
    String extension = payload.image_format.Replace(@"image/", "").Replace(@";base64", "");

    Byte[] fileContents = Convert.FromBase64String(payload.image_data);
    FileStream fileStream = new FileStream(outputDir + @"\" + filename + "." + extension, FileMode.CreateNew);
    fileStream.Write(fileContents, 0, fileContents.Length);
    fileStream.Flush();

    CarsForSale newCar = payload as CarsForSale;
    db.Cars.Add(newCar);
    await db.SaveChangesAsync();
    response.StatusCode = 200;
    response.Headers.Location = $"cars/{newCar.id}";
})
.Accepts<CarsForSale>("application/json")
.Produces<CarsForSale>(StatusCodes.Status201Created)
.WithName("AddNewCar").WithTags("Setters");


// Add a new Customer to database
app.MapPost("/customers", async ([FromBody] Customer newCustomer, [FromServices] CarDealershipDB db, HttpResponse response) => {
    db.Customers.Add(newCustomer);
    await db.SaveChangesAsync();
    response.StatusCode = 200;
    response.Headers.Location = $"customers/{newCustomer.id}";
})
.Accepts<Customer>("application/json")
.Produces<Customer>(StatusCodes.Status201Created)
.WithName("AddNewCustomer").WithTags("Setters");


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


app.UseRouting();
app.Run();
