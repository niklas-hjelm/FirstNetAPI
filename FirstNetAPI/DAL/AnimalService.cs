using FirstNetAPI.Models;

namespace FirstNetAPI.DAL
{
    class AnimalService : IAnimalService
    {
        private readonly AnimalContext _context;

        public AnimalService()
        {
            _context = new AnimalContext();
        }

        public void Create(Animal? animal)
        {
            if (animal is null)
            {
                return;
            }

            _context.Animals.Add(animal);
            _context.SaveChanges();
        }

        public Animal? GetById(int id)
        {
            return _context.Animals.FirstOrDefault(a => a.Id == id);
        }

        public List<Animal> GetAll()
        {
            return _context.Animals.Where(a => true).ToList();
        }

        public void Update(Animal animal)
        {
            var existingAnimal = GetById(animal.Id);
            if (existingAnimal is null)
            {
                return;
            }

            existingAnimal.Name = animal.Name;
            existingAnimal.Type = animal.Type;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var existingAnimal = GetById(id);
            if (existingAnimal is null)
            {
                return;
            }
            _context.Remove(existingAnimal);
            _context.SaveChanges();
        }
    }
}
