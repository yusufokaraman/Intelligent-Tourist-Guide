﻿using ITG.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Entities.Dtos
{
    public class PlaceAddDto
    {
        [DisplayName("Mekan Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(70, ErrorMessage = "{0} alanı {1} karakterden uzun olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden kısa olmamalıdır.")]
        public string Name { get; set; }

        [DisplayName("Adres")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(500, ErrorMessage = "{0} alanı {1} karakterden uzun olmamalıdır.")]
        [MinLength(10, ErrorMessage = "{0} alanı {1} karakterden kısa olmamalıdır.")]
        public string Address { get; set; }

        [DisplayName("Mekan Görsel")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(250, ErrorMessage = "{0} alanı {1} karakterden uzun olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden kısa olmamalıdır.")]
        public string PlacePicture { get; set; }

        [DisplayName("Şehir")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public int CityId { get; set; }
        public City City { get; set; }

        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
