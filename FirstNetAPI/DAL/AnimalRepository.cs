using FirstNetAPI.Models;

namespace FirstNetAPI.DAL
{
    class AnimalRepository
    {
        private readonly AnimalContext _context;

        public AnimalRepository()
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
            return _context.Animals.FirstOrDefault(a => a.ID == id);
        }

        public List<Animal?> GetAll()
        {
            return _context.Animals.Where(a=>true).ToList();
        }

        public void Update(Animal animal)
        {
            var existingAnimal = GetById(animal.ID);
            if (existingAnimal is null)
            {
                return;
            }

            _context.Update(animal);
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
