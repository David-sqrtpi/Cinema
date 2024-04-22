using Cinema.Web.Services;

var builder = WebApplication.CreateBuilder(args);

#warning Use the base address right here
builder.Services.AddHttpClient<IMovieService, MovieService>(c => { c.BaseAddress = new Uri(builder.Configuration["Urls:Movies"]!); });
builder.Services.AddHttpClient<IFunctionService, FunctionService>(c => { c.BaseAddress = new Uri(builder.Configuration["Urls:Functions"]!); });
builder.Services.AddHttpClient<ITicketService, TicketService>(c => { c.BaseAddress = new Uri(builder.Configuration["Urls:Tickets"]!); });

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}/{id2?}");

app.Run();
