using test_app.Infrastructure;
using Microsoft.EntityFrameworkCore;
using test_app.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DbConnection"]);
});
    

var app = builder.Build();

const string BASEURL = "/api/products";
app.MapGet($"{BASEURL}/{{id}}", async(HttpContext context,DataContext data)=>
{
    string id = context.Request.RouteValues["id"] as string;
    if(id != null)
    {
        Product product = data.Products.Find(long.Parse(id));
        if(product == null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }
        else
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize<Product>(product));
        }
    }
});

//for all products

app.MapGet($"{BASEURL}", async (HttpContext context, DataContext data) =>
{
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsync(JsonSerializer.Serialize<IEnumerable<Product>>(data.Products));
});
app.MapPost(BASEURL, async (HttpContext context, DataContext data) =>
{
    Product product = await JsonSerializer.DeserializeAsync<Product>(context.Request.Body);
    if(product != null)
    {
        await data.AddAsync(product);
        await data.SaveChangesAsync();
        context.Response.StatusCode = StatusCodes.Status200OK;
    }
   
});

//posle pravljenja obj
var context  = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

app.MapGet("/", () => "Hello World!");

app.Run();
