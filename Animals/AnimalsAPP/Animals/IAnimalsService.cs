namespace AnimalsAPP.Animals;

public interface IAnimalsService
{
    IEnumerable<Animal> GetStudents();
    int CreateStudent(Animal student);
    Animal? GetStudent(int idStudent);
    int UpdateStudent(Animal student);
    int DeleteStudent(int idStudent);
}

