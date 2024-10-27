using System.ComponentModel.DataAnnotations;

namespace HappyWarehouseCore.Dtos
{
    public class AddRoleModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}