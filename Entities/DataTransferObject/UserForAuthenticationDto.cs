using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObject
{
    public record UserForAuthenticationDto
    {
        [Required(ErrorMessage = "UserName is required")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
    }
}
