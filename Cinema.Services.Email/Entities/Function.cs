namespace Cinema.Services.Email.Entities;

public partial class Function
{
    public Guid FunctionId { get; set; }

    public Guid MovieId { get; set; }

    public decimal Price { get; set; }

    public DateTime FunctionDate { get; set; }

    public int AvailableSeats { get; set; }
}
