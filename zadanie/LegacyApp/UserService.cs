using System;

namespace LegacyApp
{
    public class UserService
    {
        private string firstName;
        private string lastName;
        private string email;
        private DateTime dateOfBirth;
        private int clientId;
        DateTime now = DateTime.Now;
        private Client client;
        private User user;
        
        void InitializeUserService(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.dateOfBirth = dateOfBirth;
            this.clientId = clientId;
            
        }
        
        
        public UserService()
        {
        }
        
        public bool CheckNameAndEmailValidity()
        {   
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }
             if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }
            return true;
            
        }

        bool VerifyAge()
        {
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            
            if (age < 21)
            {
                return false;
            }
            return true;
        }

        Client GetClientFromRepository()
        {
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);
            return client;
        }

        User createUser(Client client)
        {
            
            User user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            return user;
        }

        void CheckUserCreditLimit()
        {
            client = GetClientFromRepository();
            user = createUser(client);
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }
        }

        bool ValidateCreditLimit()
        {
            CheckUserCreditLimit();
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            return true;
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            InitializeUserService(firstName, lastName, email, dateOfBirth, clientId);
            
            if (CheckNameAndEmailValidity() && VerifyAge() && ValidateCreditLimit())
            {
                UserDataAccess.AddUser(user);
                return true;
            }
            
            return false;
        }
    }
}
