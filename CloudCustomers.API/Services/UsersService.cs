﻿using CloudCustomers.API.Models;

namespace CloudCustomers.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;
        public UsersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<User>> GetAllUsers()
        {
           
            throw new NotImplementedException();
        }
    }

  
}