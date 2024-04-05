using LegacyApp;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LegacyAppTest;

public class UserServiceTest
{
    
    private string firstName = "Anna";
    private string lastName = "Kowalski";
    private string email = "anna.maria@o2.pl";
    private DateTime dateOfBirth = DateTime.Parse("1982-03-21");
    private int clientId = 1;
    private UserService userService = new UserService();
    
    [Fact]
    public void AddUser_Return_False_If_FirstName_Or_LastName_is_NullOrEmpty()
    {
        bool result1 = userService.AddUser(null, null, email, dateOfBirth, clientId);
        bool result2 = userService.AddUser(null, lastName, email, dateOfBirth, clientId);
        bool result3 = userService.AddUser(firstName, null, email, dateOfBirth, clientId);
        bool result4 = userService.AddUser(String.Empty, String.Empty, email, dateOfBirth, clientId);
        bool result5 = userService.AddUser(String.Empty, lastName, email, dateOfBirth, clientId);
        bool result6 = userService.AddUser(firstName, String.Empty, email, dateOfBirth, clientId);
        bool result7 = userService.AddUser(null, String.Empty, email, dateOfBirth, clientId);
        bool result8 = userService.AddUser(String.Empty, null, email, dateOfBirth, clientId);

        Assert.Multiple(() =>
        {
            Assert.False(result1);
            Assert.False(result2);
            Assert.False(result3);
            Assert.False(result4);
            Assert.False(result5);
            Assert.False(result6);
            Assert.False(result7);
            Assert.False(result8);
            
        });

    }

    [Fact]
    public void AddUser_Return_False_If_Email_Not_Contains_Dot_And_At()
    {
        bool result = userService.AddUser(firstName, lastName, "annaMaria", dateOfBirth, clientId);
        Assert.False(result);
    }
    
    [Fact]   
    public void AddUser_Return_False_If_NowMonth_Is_Less_Than_BirtMonth_OR_NowMonth_Is_Equal_BorthMonth_And_NowDay_Is_Less_Than_BirthDay()
    {
        int adult_age = 21;
        DateTime test_birthday = DateTime.Now.AddYears(-adult_age);
        
        bool result1 = userService.AddUser(firstName, lastName, email, test_birthday.AddDays(1), clientId);
        bool result2 = userService.AddUser(firstName, lastName, email, test_birthday.AddMonths(1), clientId);
        
        Assert.Multiple(() =>
        {
            Assert.False(result1);
            Assert.False(result2);
        });
    }

    
    [Fact]   
    public void AddUser_return_False_If_User_HasCreditLimit_Is_True_And_User_CreditLimit_Less_Than_500()
    {
        bool result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);
        
        Assert.False(result);
    }
    
    [Fact]   
    public void AddUser_return_True_If_User_Type_Is_VeryImportantClient_Or_ImportantClient()
    {
        bool result1 = userService.AddUser(firstName, lastName, email, dateOfBirth, 3);
        bool result2 = userService.AddUser(firstName, lastName, email, dateOfBirth, 2);
        
        Assert.Multiple(() =>
        {
            Assert.True(result1);
            Assert.True(result2);
        });

    }
    
}