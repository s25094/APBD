namespace AnimalsAPP.Animals;

public class IAnimalsRepository
{
    IEnumerable<Student> GetStudents();
    int CreateStudent(Student student);
    Student GetStudent(int idStudent);
    int UpdateStudent(Student student);
    int DeleteStudent(int idStudent);
}
