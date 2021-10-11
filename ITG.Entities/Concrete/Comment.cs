using ITG.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Entities.Concrete
{
    public class Comment:EntityBase, IEntity
    {
        public string Text { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }

    }
}
