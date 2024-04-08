﻿using LegacyApp;
using System;

namespace LegacyAppConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * DO NOT CHANGE THIS FILE AT ALL
             */

            var userService = new UserService();
            var addResult = userService.AddUser("John", "Kowalski", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 3);
            if (addResult)
                Console.WriteLine($"Adding John Doe was successful");
            else
                Console.WriteLine($"Adding John Doe was unsuccessful");
        }
    }
}
