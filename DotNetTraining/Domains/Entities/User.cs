using Dapper.Contrib.Extensions;

namespace DotNetTraining.Domains.Entities
{
    [Table("user")]
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string Email { get; set; } 
        public string Password { get; set; }
    }
}
