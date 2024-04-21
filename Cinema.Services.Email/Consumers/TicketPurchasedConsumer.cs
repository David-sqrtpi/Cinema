using System.Net;
using System.Net.Mail;
using Cinema.Integration.Events;
using Cinema.Services.Email.Entities;
using Cinema.Services.Email.Services;
using MassTransit;

namespace Cinema.Services.Email.Consumers;

public class TicketPurchasedConsumer(
    IFunctionService functionService,
    IMovieService movieService,
    ITicketService ticketService,
    IConfiguration configuration) : IConsumer<TicketPurchasedEvent>
{
    private readonly string Email = configuration["smtp:email"];

    public async Task Consume(ConsumeContext<TicketPurchasedEvent> context)
    {
        
        var smtp = ConfigureSmtp();
        var ticket = await GetTicketInformation(context.Message.TicketId);
        var message = BuildMessage(ticket);
        smtp.Send(message);

        Console.WriteLine("E-mail sent!");
    }

    private SmtpClient ConfigureSmtp()
    {
        var password = configuration["smtp:password"];
        var host = configuration["host"];

        using var smtp = new SmtpClient();
        smtp.Host = host;
        smtp.Port = 587;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.UseDefaultCredentials = false;
        smtp.EnableSsl = true;
        smtp.Credentials = new NetworkCredential(Email, password);

        return smtp;
    }


    public async Task<TicketDTO> GetTicketInformation(Guid ticketId)
    {
        var ticket = await ticketService.GetById(ticketId);
        var function = await functionService.GetById(ticket.FunctionId);
        var movie = await movieService.GetById(function.MovieId);

        return new TicketDTO
        {
            TicketId = ticket.TicketId,
            Function = new FunctionDTO
            {
                FunctionId = function.FunctionId,
                Movie = movie,
                Price = function.Price,
                FunctionDate = function.FunctionDate,
                AvailableSeats = function.AvailableSeats
            },
            AdditionalPrice = ticket.AdditionalPrice,
            Seats = ticket.Seats,
            UserName = ticket.UserName,
            Email = ticket.Email
        };

    }
    
    private MailMessage BuildMessage(TicketDTO ticket)
    {
        using var message = new MailMessage(from: new MailAddress(Email), to: new MailAddress(ticket.Email));
        message.IsBodyHtml = true;
        message.Subject = "Movie App - You have new notifications";
        message.Body = BuildHTML(ticket);

        return message;
    }

    public static string BuildHTML(TicketDTO ticket)
    {
        return $@"
            <body>
                <h1>Hello {ticket.UserName}!</h1>
                <h3>You have made a reservation for {ticket.Function.Movie.Title}</h3>
                <p>You have reserved {ticket.Seats} seats</p>
                <p>Function date is {ticket.Function.FunctionDate:dddd, dd MMMM yyyy 'at' HH:MM tt}</p>
                <p>Your final cost was {ticket.Function.Price * ticket.Seats:C}</p>
            </body>

            <style>
                body {{
                    font-family: system-ui, -apple-system, ""Segoe UI"", Roboto, ""Helvetica Neue"", Arial, ""Noto Sans"", ""Liberation Sans"", sans-serif, ""Apple Color Emoji"", ""Segoe UI Emoji"", ""Segoe UI Symbol"", ""Noto Color Emoji"";
                }}
            </style>
        ";
    }
}
