using Microsoft.AspNetCore.Mvc;
using Cinema.Web.Models;
using Cinema.Web.Services;

namespace Cinema.Web.Controllers;

public class MoviesController(IMovieService movieService) : Controller
{
    // GET: Movies
    public async Task<IActionResult> Index()
    {
        var movies = await movieService.GetAll();
        return View(movies);
    }

    // GET: Movies/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id is null)
            return NotFound();

        var movie = await movieService.GetById(id);

        if (movie is null)
            return NotFound();

        return View(movie);
    }

    // GET: Movies/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Movies/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Year,Genre,Director,Cast,PosterImage")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            await movieService.Create(movie);
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // GET: Movies/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id is null)
            return NotFound();

        var movie = await movieService.GetById(id);
        if (movie is null)
            return NotFound();

        return View(movie);
    }

    // POST: Movies/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("MovieId,Title,Year,Genre,Director,Cast,PosterImage")] Movie movie)
    {
        if (id != movie.MovieId)
            return NotFound();

        if (ModelState.IsValid)
        {
            await movieService.Update(movie);
            //try
            //{
            //    context.Update(movie);
            //    await context.SaveChangesAsync();

            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!MovieExists(movie.MovieId))
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
        return View(movie);
    }

    // GET: Movies/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id is null)
            return NotFound();

        var movie = await movieService.GetById(id);

        if (movie is null)
            return NotFound();

        return View(movie);
    }

    // POST: Movies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await movieService.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}
