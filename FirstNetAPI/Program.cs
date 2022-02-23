using FirstNetAPI.DAL;
using FirstNetAPI.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAnimalService, AnimalService>();
var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", () => Results.Redirect("index.html"));

app.MapGet("/animals", (IAnimalService repo) => repo.GetAll());

app.MapGet("/animals/{id}", (IAnimalService repo, int id) =>
{
    var animal = repo.GetById(id);

    return Results.Ok(animal);
});

app.MapPost("/animals", (IAnimalService repo, Animal animal) =>
{
    repo.Create(animal);
    var animals = repo.GetAll();
    return Results.Created($"animals/{animal.Id}",animals);
});

app.MapPut("/animals", (IAnimalService repo, Animal animal) =>
{
    repo.Update(animal);
    return Results.Ok();
});

app.MapDelete("/animals/{id}", (IAnimalService repo, int id) =>
{
    repo.Delete(id);
    var animals = repo.GetAll();
    return Results.Accepted($"/animals/{id}", animals);
});

app.Run();