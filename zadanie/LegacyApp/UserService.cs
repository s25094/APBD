using System;

namespace LegacyApp
{
    
    public class UserService
    {
        
        private User user;
        
        int GetUserCreditLimit(string lastName, DateTime dateOfBirth)
        {
            return new UserCreditService().GetCreditLimit(lastName, dateOfBirth);
        }

        string GetUserType(User user)
        {
            return user.Client.GetType().ToString();
        }
        
        void EvaluateUserCreditLimit(User user)
        {
            string UserType = GetUserType(user);
            Console.WriteLine("here");
            Console.WriteLine(string.IsNullOrEmpty(UserType));
            if (!UserType.Equals("VeryImportantClient"))
            {
                user.CreditLimit = GetUserCreditLimit(user.LastName, user.DateOfBirth);
                if (UserType.Equals("ImportantClient"))
                {
                    user.CreditLimit *= 2;
                    Console.WriteLine(user.CreditLimit);
                }
                else
                {
                    user.HasCreditLimit = true;
                }
            }
        }

        bool AssessCreditworthiness(User user)
        {
            EvaluateUserCreditLimit(user);
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            return true;
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            user = new User(firstName, lastName, email, dateOfBirth, new ClientRepository().GetById(clientId));
            Client client = new ClientRepository().GetById(clientId);
            Console.WriteLine(client.Type);
            if (user.isUserCreated && AssessCreditworthiness(user)) 
            {
                UserDataAccess.AddUser(user);
                return true;
                
            }

            return false;
        }
    }
}
