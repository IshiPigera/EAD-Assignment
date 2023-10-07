using MongoDB.Bson.Serialization.Attributes;

namespace TravelerAppService.Models
{
    public class User
    {
        public string Id { get; set; } 
        public string NationalIdentificationCard { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; } 
    }
}
