using System.ComponentModel.DataAnnotations;

namespace HappyWarehouseCore.Dtos
{
    public class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}