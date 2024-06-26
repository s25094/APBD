using Animals.Model;
namespace Animals.Repositories;

public interface IAnimalsRepository
{
    IEnumerable<Animal> GetAnimals(string? parameter);
    int CreateAnimal(Animal animal);
    Animal GetAnimal(int idAnimal);
    int UpdateAnimal(Animal animal);
    int DeleteAnimal(int idAnimal);
    
}