using LegacyApp;

namespace LegacyAppTests;

public class UserDerviceTests
{
    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_OR_LastName_Empty()
    {
        //A
        String firstName = "";
        String lastName = "";
        String email = "joe@due.com";
        DateTime dateOfBirth = new DateTime(1982, 2, 1);
        int clientID = 1;
        var userService = new UserService();

        bool result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientID);
        
        Assert.Equal(false, result);

    }
    
    
}