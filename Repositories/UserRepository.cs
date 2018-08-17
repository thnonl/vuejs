using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Vue2Spa.Helper;
using Vue2Spa.Models;

namespace Vue2Spa.Repositories
{
    public interface IUserRepository
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(ObjectId id);
        User Create(User user, string password);
        void Update(ObjectId id, User user);
        void Delete(ObjectId id);
    }

    public class UserRepository : IUserRepository
    {
        MongoClient _client;
        IMongoDatabase _db;

        public UserRepository()
        {
            _client = new MongoClient($"mongodb://{Constants.Server.Username}:{Constants.Server.Password}@{Constants.Server.Url}");
            _db = _client.GetDatabase("pwasimpleapp");
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _db.GetCollection<User>(nameof(User)).AsQueryable().FirstOrDefault(p => p.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _db.GetCollection<User>(nameof(User)).AsQueryable();
        }

        public User GetById(ObjectId id)
        {
            return _db.GetCollection<User>(nameof(User)).AsQueryable().FirstOrDefault(p => p.Id == id);
        }

        public User Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_db.GetCollection<User>(nameof(User)).AsQueryable().Any(x => x.Username == user.Username))
                throw new AppException("Username '" + user.Username + "' is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _db.GetCollection<User>(nameof(User)).InsertOne(user);

            return user;
        }

        public void Update(ObjectId id, User userParam)
        {
            var user = _db.GetCollection<User>(nameof(User)).AsQueryable().FirstOrDefault(u => u.Id == id);

            if (user == null)
                throw new AppException("User not found");

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (_db.GetCollection<User>(nameof(User)).AsQueryable().Any(x => x.Username == userParam.Username))
                    throw new AppException("Username " + userParam.Username + " is already taken");
            }

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(userParam.Password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(userParam.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            var res = Query<User>.EQ(pd => pd.Id, id).ToBsonDocument();
            _db.GetCollection<User>(nameof(User)).ReplaceOne(res, user);
        }

        public void Delete(ObjectId id)
        {
            var user = _db.GetCollection<User>(nameof(User)).AsQueryable().FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                var res = Query<Product>.EQ(e => e.Id, id).ToBsonDocument();
                var operation = _db.GetCollection<Product>(nameof(User)).DeleteOne(res);
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}