﻿using DotnetAPI.Models;
using System.Linq;

namespace DotnetAPI.Data
{
    public class UserRepository : IUserRepository
    {
        DataContextEF _entityFramework;
        public UserRepository(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
        }

        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges() > 0;
        }

        public void AddEntity<T>(T entityToAdd)
        {
            if (entityToAdd != null) {
            _entityFramework.Add(entityToAdd);
            }
        }
        public void RemoveEntity<T>(T entityToAdd)
        {
            if (entityToAdd != null)
            {
                _entityFramework.Add(entityToAdd);
            }
        }

        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = _entityFramework.Users.ToList<User>();
            return users;

        }

        public User GetSingleUser(int userId)
        {
            User? user = _entityFramework.Users.Where(u => u.UserId == userId).FirstOrDefault<User>();
            if (user != null)
            {
                return user;
            }

            throw new Exception("Failed to get User");
        }
    }
}
