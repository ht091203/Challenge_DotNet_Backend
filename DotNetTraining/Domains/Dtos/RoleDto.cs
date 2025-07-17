using System.ComponentModel.DataAnnotations;
namespace DotNetTraining.Domains.Dtos
{
    public class RoleDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}
