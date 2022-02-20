using FirstNetAPI.Models;

namespace FirstNetAPI.DAL
{
    class AnimalSqlRepository
    {
        private readonly AnimalContext _context;

        public AnimalSqlRepository()
        {
            _context = new AnimalContext();
        }

        public void Create(AnimalRow? animal)
        {
            if (animal is null)
            {
                return;
            }

            _context.Animals.Add(animal);
            _context.SaveChanges();
        }

        public AnimalRow? GetById(int id)
        {
            return _context.Animals.FirstOrDefault(a => a.Id == id);
        }

        public List<AnimalRow> GetAll()
        {
            return _context.Animals.Where(a => true).ToList();
        }

        public void Update(AnimalRow animal)
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
