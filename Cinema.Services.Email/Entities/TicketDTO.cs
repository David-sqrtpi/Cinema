namespace Cinema.Services.Email.Entities;

public class TicketDTO
{
    public Guid TicketId { get; set; }

    public FunctionDTO Function { get; set; } = null!;

    public decimal? AdditionalPrice { get; set; }

    public string UserName { get; set; } = null!;

    public int Seats { get; set; }

    public string Email { get; set; } = null!;
}
