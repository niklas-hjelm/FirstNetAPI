using FirstNetAPI.Models;

namespace FirstNetAPI.DAL;

internal interface IAnimalService
{
    void Create(Animal? animal);
    Animal? GetById(int id);
    List<Animal> GetAll();
    void Update(Animal animal);
    void Delete(int id);
}