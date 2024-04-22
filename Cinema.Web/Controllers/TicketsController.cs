using Cinema.Web.Models;
using Cinema.Web.Models.ViewModels;
using Cinema.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers
{
    public class TicketsController(
        IFunctionService functionService,
        ITicketService ticketService,
        IMovieService movieService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ViewFunctions(Guid? id) {
            var functions = await functionService.GetAll(id);
            var movie = await movieService.GetById(functions.FirstOrDefault().MovieId);

            var functionsViewModel = functions.Select(
                x => new IndexFunctionViewModel
                {
                    FunctionId = x.FunctionId,
                    Movie = movie,
                    FunctionDate = x.FunctionDate,
                    Price = x.Price,
                    AvailableSeats = x.AvailableSeats
                })
                .Where(x => x.Movie is not null);

            return View(functionsViewModel);
        }

        public async Task<IActionResult> Buy(Guid? functionId)
        {
#warning validate if a movie was received
            var function = await functionService.GetById(functionId);
            var movie = await movieService.GetById(function.MovieId);

            var ticket = new TicketViewModel
            {
                Movie = movie,
                Function = function,
                FunctionId = function.FunctionId
            };

            return View(ticket);
        }

        public async Task<IActionResult> Confirm([Bind("FunctionId,UserName,Email,Seats")] TicketViewModel ticketViewModel)
        {
            if (ModelState.IsValid)
            {
                var ticketForCreation = new Ticket
                {
                    FunctionId = ticketViewModel.FunctionId,
                    UserName = ticketViewModel.UserName,
                    Email = ticketViewModel.Email,
                    Seats = ticketViewModel.Seats
                };

                var ticketCreated = await ticketService.Create(ticketForCreation);

                if (ticketCreated)
                    return View(new ConfirmationViewModel
                    {
                        Message = "A confirmation was sent to your email"
                    });
            }
            
            return View(new ConfirmationViewModel
            {
                Message = "There are no available seats for this function"
            });
        }

        public async Task<IActionResult> Listing()
        {
            var functions = await functionService.GetAll();
            var listingMovies = functions.DistinctBy(x => x.MovieId).Select(x => x.MovieId);

            var movies = await movieService.GetAll(listingMovies);

            return View(movies);
        }
    }
}
