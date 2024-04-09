using System;
using System.Linq;

namespace LegacyApp
{
    
    public class UserService
    {
        private User user;
        private bool UserAdded = false;

        User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId) {
            return new User(firstName, lastName, email, dateOfBirth, CreateClientByID(clientId));
        }
        bool IsUserCreated() { return user.isUserCreated; }
        Client CreateClientByID(int clientID) { return new ClientRepository().GetById(clientID); }
        Client GetClientInfoFromUser() { return (Client) user.Client; }
        int GetCreditLimit() { return new UserCreditService().GetCreditLimit(user.LastName, user.DateOfBirth); }
        void SetUserLimit() {
            String ClientType = GetClientInfoFromUser().Type;
            if (!ClientType.Equals("VeryImportantClient")) {
                user.CreditLimit = GetCreditLimit();
                if (ClientType.Equals("ImportantClient")) {
                    user.CreditLimit *= 2;
                }
                else { user.HasCreditLimit = true; }
            }
        }
        bool AssessCreditworthiness()
        {
            if (user.HasCreditLimit && user.CreditLimit < 500) {
                return false;
            } return true;
        }

        void AddUserToDataAccess() { 
            UserDataAccess.AddUser(user);
            UserAdded = true;
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId) {
            user = CreateUser(firstName, lastName, email, dateOfBirth, clientId);
            if (IsUserCreated()) {
                SetUserLimit();
                if (AssessCreditworthiness()) {
                    AddUserToDataAccess();
                }
            }
            return UserAdded;
        }
    }
}
