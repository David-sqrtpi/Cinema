namespace Cinema.Services.Email.Entities;

public class FunctionDTO
{
    public Guid FunctionId { get; set; }

    public Movie Movie { get; set; }

    public decimal Price { get; set; }

    public DateTime FunctionDate { get; set; }

    public int AvailableSeats { get; set; }
}
