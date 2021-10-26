using ITG.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Entities.Dtos
{
    public class CategoryAddDto
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage ="{0} Boş Geçilmemelidir.")]
        [MaxLength(70,ErrorMessage ="{0} {1} karakterden uzun olmamalıdır.")]
        [MinLength(3,ErrorMessage ="{0} {1} karakterden kısa olmamalıdır.")]
        public string Name { get; set; }
       
        [DisplayName("Kategori Açıklaması")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden uzun olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden kısa olmamalıdır.")]
        public string  Description { get; set; }
        
        [DisplayName("Kategori Özel Not Alanı")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden uzun olmamaldır.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden kısa olmamalıdır.")]
        public string Note { get; set; }
       
        [DisplayName("Aktif mi?")]
        [Required(ErrorMessage = "{0} Boş Geçilmemelidir.")]
        public bool IsActive { get; set; }

        [DisplayName("Şehir")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
