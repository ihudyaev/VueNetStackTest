using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ihud.WebApi.Models
{
    public partial class User
    {
        public User()
        {
            Topic = new HashSet<Topic>();
            TopicReply = new HashSet<TopicReply>();
        }

        public static string GetPasswordHash( string password )
        {
            byte[] salt = new byte[128 / 8];
            salt = Encoding.ASCII.GetBytes("NZsP6NnmfBuYeJrrAKNuVQ==");
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        public User( UserRegisterForm rfrm )
        {
            Topic = new HashSet<Topic>();
            TopicReply = new HashSet<TopicReply>();
            DisplayName = rfrm.username;
            UserName = rfrm.username;
            Disabled = false;
            RoleId = 1;
            Email = rfrm.email;
            Password= User.GetPasswordHash(rfrm.password);

        }

        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool Disabled { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Topic> Topic { get; set; }
        public virtual ICollection<TopicReply> TopicReply { get; set; }
    }


    public partial class UserAnswer
    {
        public Profile profile { get; set; }
        public ProfileFull profilefull { get; set; }
        public string token { get; set; }
    }

    [JsonObject(Title = "str")]
    public partial class UserSubmit
    {
        [JsonProperty(PropertyName = "Email")]
        public string email { get; set; }
        [JsonProperty(PropertyName = "Password")]
        public string password { get; set; }
    }

    [JsonObject(Title = "str")]
    public partial class UserRegisterCheck
    {
        [JsonProperty(PropertyName = "Email")]
        public string email { get; set; }
        [JsonProperty(PropertyName = "Username")]
        public string username { get; set; }
    }

    [JsonObject(Title = "str")]
    public partial class ProfileUpdateForm
    {

        [JsonProperty(PropertyName = "DisplayName")]
        public string displayname { get; set; }
        [JsonProperty(PropertyName = "Birthdate")]
        public DateTime? birthdate { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        [JsonProperty(PropertyName = "Surname")]
        public string surname { get; set; }
    }

    [JsonObject(Title = "str")]
    public partial class UserRegisterForm
    {
        [JsonProperty(PropertyName = "Email")]
        public string email { get; set; }
        [JsonProperty(PropertyName = "Username")]
        public string username { get; set; }

        [JsonProperty(PropertyName = "Password")]
        public string password { get; set; }
    }

    public partial class UserMessageAnswer
    {
        public UserMessageAnswer()
        {
            this.Messages = new List<string>();
        }
        public List<string> Messages { get; set; }

    }

    public class ProfileFull
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool Disabled { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int TopicCnt { get; set; }
        public int TopicReplyCnt { get; set; }
        public ProfileFull( User usr )
        {
            this.DisplayName = usr.DisplayName;
            this.Email = usr.Email;
            this.TopicCnt = usr.Topic.Count;
            this.TopicReplyCnt = usr.TopicReply.Count;
            this.Birthdate = usr.Birthdate;
            this.Name = usr.Name;
            this.Surname = usr.Surname;
          
        }
    }

    public class Profile
    {
        public string DisplayName { get; set; }
        
        public string Email { get; set; }
        public string Slogan { get; set; }
        public int TopicCnt { get; set; }
        public int TopicReplyCnt { get; set; }
        public Profile( User usr )
        {
            this.DisplayName = usr.DisplayName;
            this.Email = usr.Email;
            this.TopicCnt = usr.Topic.Count;
            this.TopicReplyCnt = usr.TopicReply.Count;
            var random = new Random();
            var First = new List<string>{
                        "Run",
                        "Stop",
                        "Be good"};
            var Second = new List<string>{
                        "Forest",
                        "Suhov",
                        "Johnny"};
            var Third = new List<string>{
                        "?",
                        "!",
                        "."};
            int index = random.Next(First.Count);
            this.Slogan += First[index] + " ";
            index = random.Next(Second.Count);
            this.Slogan += Second[index];
            index = random.Next(Third.Count);
            this.Slogan += Third[index];

        }
    }
}
