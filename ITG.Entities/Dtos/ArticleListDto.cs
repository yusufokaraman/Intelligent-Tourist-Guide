using ITG.Entities.Concrete;
using ITG.Shared.Entities.Abstract;
using ITG.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Entities.Dtos
{
    public class ArticleListDto:DtoGetBase
    {
        public IList<Article> Articles { get; set; }
        public int? CategoryId { get; set; }
        public int? CityId { get; set; }

    }
}
