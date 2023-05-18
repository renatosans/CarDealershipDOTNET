namespace CarDealershipDOTNET.Models;

public class VehiclePayload : CarsForSale
{
    // Snakecase é usado no RUST, como o frontend foi feito inicialmente para se comunicar com o
    // backend em RUST mantive o Snakecase, em DOTNET o normal seria Pascal case.
    public string? image_format { get; set; }
    public string? image_data { get; set; }

    public VehiclePayload () {
        this.brand = "";
        this.model = "";
        this.year = 2000;
        this.mileage = 0;
        this.color = "branco";
        this.price = 0;
    }

}
