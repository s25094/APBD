using Animals.Model;
namespace Animals.Services;

public interface IAnimalsService
{
    IEnumerable<Animal> GetAnimals(string? parameter);
    int CreateAnimal(Animal animal);
    Animal? GetAnimal(int idAnimal);
    int UpdateAnimal(Animal animal);
    int DeleteAnimal(int idAnimal);
}
