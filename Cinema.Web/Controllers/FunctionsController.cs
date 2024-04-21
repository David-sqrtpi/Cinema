using Microsoft.AspNetCore.Mvc;
using Cinema.Web.Models;
using Cinema.Web.Services;
using Cinema.Web.Models.ViewModels;

namespace Cinema.Web.Controllers;

public class FunctionsController(IFunctionService functionService, IMovieService movieService) : Controller
{
	// GET: Functions
	public async Task<IActionResult> Index()
	{
		var functions = await functionService.GetAll();
		var moviesIds = functions.Select(x => x.MovieId);
		var movies = await movieService.GetAll(moviesIds);

		var functionsViewModel = functions.Select(
			x => new IndexFunctionViewModel
			{
				FunctionId = x.FunctionId,
				Movie = movies.FirstOrDefault(y => y.MovieId == x.MovieId),
				Price = x.Price,
				FunctionDate = x.FunctionDate,
				AvailableSeats = x.AvailableSeats
			})
			.Where(x => x.Movie is not null)
			.OrderBy(x => x.Movie.Title)
			.ThenBy(x => x.FunctionDate);

		return View(functionsViewModel);
	}

	// GET: Functions/Details/5
	public async Task<IActionResult> Details(Guid? id)
	{
		if (id == null)
			return NotFound();

		var function = await functionService.GetById(id);
		if (function is null) return NotFound();

		var movie = await movieService.GetById(function.MovieId);
		if (movie is null) return NotFound();

		var functionViewModel = new IndexFunctionViewModel
		{
			FunctionId = function.FunctionId,
			Movie = movie,
			Price = function.Price,
			FunctionDate = function.FunctionDate,
			AvailableSeats = function.AvailableSeats
		};

		return View(functionViewModel);
	}

	// GET: Functions/Create
	public async Task<IActionResult> Create()
	{
		var movies = await movieService.GetAll();

		var viewModel = new FunctionViewModel
		{
			Movies = movies
		};

		return View(viewModel);
	}

	// POST: Functions/Create
	// To protect from overposting attacks, enable the specific properties you want to bind to.
	// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("MovieId,Price,FunctionDate,AvailableSeats")] Function function)
	{
		if (ModelState.IsValid)
		{
			await functionService.Create(function);
			return RedirectToAction(nameof(Index));
		}
		return View(function);
	}

	// GET: Functions/Edit/5
	public async Task<IActionResult> Edit(Guid? id)
	{
		if (id == null)
			return NotFound();


		var function = await functionService.GetById(id);
		if (function == null)
			return NotFound();

		var movies = await movieService.GetAll();

		var viewModel = new FunctionViewModel
		{
			Movies = movies,
			Function = function
		};

		return View(viewModel);
	}

	// POST: Functions/Edit/5
	// To protect from overposting attacks, enable the specific properties you want to bind to.
	// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(Guid id, [Bind("FunctionId,MovieId,Price,FunctionDate,AvailableSeats")] Function function)
	{
		if (id != function.FunctionId)
			return NotFound();

		if (ModelState.IsValid)
		{
			await functionService.Update(function);
			//try
			//{
			//    _context.Update(function);
			//    await _context.SaveChangesAsync();
			//}
			//catch (DbUpdateConcurrencyException)
			//{
			//    if (!FunctionExists(function.FunctionId))
			//    {
			//        return NotFound();
			//    }
			//    else
			//    {
			//        throw;
			//    }
			//}
			return RedirectToAction(nameof(Index));
		}
		return View(function);
	}

	// GET: Functions/Delete/5
	public async Task<IActionResult> Delete(Guid? id)
	{
		if (id == null)
			return NotFound();

		var function = await functionService.GetById(id);
		if (function == null)
			return NotFound();

		return View(function);
	}

	// POST: Functions/Delete/5
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(Guid id)
	{
		await functionService.Delete(id);
		return RedirectToAction(nameof(Index));
	}
}
