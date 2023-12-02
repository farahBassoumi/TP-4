using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using tp.Data;
using tp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddMvc(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    options.EnableEndpointRouting = false; // Add this line if not using Endpoint Routing
}).SetCompatibilityVersion(CompatibilityVersion.Latest);



builder.Services.AddDbContext<ApplicationDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesConnectionString"
)));



builder.Services.AddLogging(builder =>
{

    builder.AddConsole();
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();//enable routing in ap.net 

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{

    endpoints.MapControllerRoute(
    name: "editMovie",
    pattern: "Movie/Edit/{id}", // Define your desired URL pattern here
    defaults: new { controller = "Movie", action = "Edit" }// Specify the controller and action names
    );

    endpoints.MapControllerRoute(
    name: "ByReleaseMovie",
    pattern: "Movie/ByRelease/{year}/{month}", // Define your desired URL pattern here
    defaults: new { controller = "Movie", action = "ByRelease" }// Specify the controller and action names
    );

    endpoints.MapControllerRoute(
   name: "MovieCustomer",
   pattern: "Movie/GetCustomer", // Define your desired URL pattern here
   defaults: new { controller = "Movie", action = "GetCustomer" }// Specify the controller and action names
   );
    endpoints.MapControllerRoute(
  name: "AddMovie",
  pattern: "Movie/AddMovie", // Define your desired URL pattern here
  defaults: new { controller = "Movie", action = "AddMovie" }// Specify the controller and action names
  );
    endpoints.MapControllerRoute(
name: "AddGenre",
pattern: "Movie/AddGenre", // Define your desired URL pattern here
defaults: new { controller = "Movie", action = "AddGenre" }// Specify the controller and action names
);
    endpoints.MapControllerRoute(
name: "Index",
pattern: "Customer/Index", // Define your desired URL pattern here
defaults: new { controller = "Customer", action = "Index" }// Specify the controller and action names
);
    endpoints.MapControllerRoute(
name: "Create",
pattern: "Customer/Create", // Define your desired URL pattern here
defaults: new { controller = "Customer", action = "Create" }// Specify the controller and action names
);
    endpoints.MapControllerRoute(
name: "Create",
pattern: "MembershipType/Create", // Define your desired URL pattern here
defaults: new { controller = "MembershipType", action = "Create" }// Specify the controller and action names
);

    endpoints.MapControllerRoute(
name: "Upload",
pattern: "Movie/Upload", // Define your desired URL pattern here
defaults: new { controller = "Movie", action = "Upload" }// Specify the controller and action names
);













    /* endpoint.Map("Home", async (context) =>//execute sin all calles : get,post...
     {
         await context.Response.WriteAsync("you are in home page");
     });
     endpoint.MapGet("Home/{id}",  async (context) =>// will only execute in the case of get call; same thing for others
     {//id is an object type so we need to convert it to a datatype(convert.ToInt32)
        var id= Convert.ToInt32(context.Request.RouteValues["id"]);
        await context.Response.WriteAsync("you are in home page"+id);
     });*/
});


//app.Run(async (HttpContext context) =>// executes when the URL is ; https://localhost:7047/
//{
//    await context.Response.WriteAsync("URL not found");
//} );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movie}/{action=Index}");


app.Run();
