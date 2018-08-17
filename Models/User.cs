using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Vue2Spa.Models
{
    public class User
    {
        public ObjectId Id { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }

        [BsonElement("PasswordHash")]
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }

        [BsonElement("PasswordSalt")]
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }

        [BsonIgnore]
        public string Password { get; set; }
    }
}
