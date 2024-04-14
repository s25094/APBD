namespace AnimalsAPP.Animals;

public class AnimalsService : IAnimalsService
{
    private readonly IAnimalsRepository _studentsRepository;
    
    public AnimalsService(IAnimalsRepository studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }
    
    public IEnumerable<Animal> GetStudents()
    {
        //Business logic
        return _studentsRepository.GetStudents();
    }
    
    public int CreateStudent(Student student)
    {
        //Business logic
        return _studentsRepository.CreateStudent(student);
    }

    public Animal? GetStudent(int idStudent)
    {
        //Business logic
        return _studentsRepository.GetStudent(idStudent);
    }

    public int UpdateStudent(Student student)
    {
        //Business logic
        return _studentsRepository.UpdateStudent(student);
    }

    public int DeleteStudent(int idStudent)
    {
        //Business logic
        return _studentsRepository.DeleteStudent(idStudent);
    }
}