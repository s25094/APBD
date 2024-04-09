using System;
using System.Linq;

namespace LegacyApp
{
    public class User
    {
        public object Client { get; internal set; }
        public DateTime DateOfBirth { get; internal set; }
        public string EmailAddress { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public bool HasCreditLimit { get; internal set; }
        public int CreditLimit { get; internal set; }
        public bool isUserCreated { get; internal set; }
        
        
        public User(string firstName, string lastName, string emailAddress, DateTime dateOfBirth, Client client)
        {
            isUserCreated = false;
            bool checkIfFirstNameorLastNameNUllorEmpty = new []{firstName, lastName}.All(s => !string.IsNullOrEmpty(s));
            bool isEmailAddresVerified = new[] {"@", ".",}.All(c => emailAddress.Contains(c));
            bool isAgeVerified = UserAge(dateOfBirth) >= 21;

            if (new[] {checkIfFirstNameorLastNameNUllorEmpty, isEmailAddresVerified, isAgeVerified}.All(b => b))
            {
                InitializeData(firstName, lastName, emailAddress, dateOfBirth, client);
                isUserCreated = true;
            }
            
        }

        void InitializeData(string firstName, string lastName, string emailAddress, DateTime dateOfBirth, Client client)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            DateOfBirth = dateOfBirth;
            Client = client;
        }
        

        int UserAge(DateTime dateOfBirth)
        {
            DateTime now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month ||
                (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            {
                return age--;
            }

            return age;
        }
        
    }
}