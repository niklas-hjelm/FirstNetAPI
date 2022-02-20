using FirstNetAPI.DAL;
using FirstNetAPI.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AnimalRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", () => Results.Redirect("index.html"));

app.MapGet("/animals", ([FromServices] AnimalRepository repo) => repo.GetAll());

app.MapGet("/animals/{id}", ([FromServices] AnimalRepository repo, int id) =>
{
    var animals = repo.GetAll();
    if (id >= animals.Count)
    {
        return Results.NotFound();
    }

    return Results.Ok(animals[id]);
});

app.MapPost("/animals", ([FromServices] AnimalRepository repo, Animal? animal) =>
{
    if (animal is null)
    {
        return Results.BadRequest();
    }

    repo.Create(animal);
    var animals = repo.GetAll();
    return Results.Created($"/animals/{animal.ID}", animals);
});

app.MapPut("/animals/{id}", ([FromServices] AnimalRepository repo, Animal animal) =>
{
    repo.Update(animal);
    return Results.Ok();
});

app.MapDelete("/animals/{id}", ([FromServices] AnimalRepository repo, int id) =>
{
    repo.Delete(id);
    var animals = repo.GetAll();
    return Results.Accepted($"/animals/{id}", animals);
});

app.Run();