using System.ComponentModel.DataAnnotations;

namespace Ecomweb.Data.Dto
{
    public class GoogleLoginDto
    {
        [Required]
        public string IdToken { get; set; } = null!;
    }
}