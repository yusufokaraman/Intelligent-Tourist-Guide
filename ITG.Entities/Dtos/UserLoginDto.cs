using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITG.Entities.Dtos
{
    public class UserLoginDto
    {
        [DisplayName("E-Posta Adresi")]
        [Required(ErrorMessage = "{0} Boş Geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden uzun olmamalıdır.")]
        [MinLength(10, ErrorMessage = "{0} {1} karakterden kısa olmamalıdır.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} Boş Geçilmemelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden uzun olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden kısa olmamalıdır.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
