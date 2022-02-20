using FirstNetAPI.DAL;
using FirstNetAPI.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AnimalSqlRepository>();
builder.Services.AddSingleton<AnimalDocRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", () => Results.Redirect("index.html"));

app.MapGet("/animals", ([FromServices] AnimalDocRepository repo) => repo.GetAll());

app.MapGet("/animals/{id}", ([FromServices] AnimalDocRepository repo, string id) =>
{
    var animal = repo.GetById(id);

    return Results.Ok(animal);
});

app.MapPost("/animals", ([FromServices] AnimalDocRepository repo, AnimalDoc animal) =>
{
    repo.Create(animal);
    var animals = repo.GetAll();
    return Results.Created($"animals/{animal.Id}",animals);
});

app.MapPut("/animals", ([FromServices] AnimalDocRepository repo, AnimalDoc animal) =>
{
    repo.Update(animal);
    return Results.Ok();
});

app.MapDelete("/animals/{id}", ([FromServices] AnimalDocRepository repo, string id) =>
{
    repo.Delete(id);
    var animals = repo.GetAll();
    return Results.Accepted($"/animals/{id}", animals);
});

app.Run();