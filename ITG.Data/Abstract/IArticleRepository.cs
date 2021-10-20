using ITG.Entities.Concrete;
using ITG.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Data.Abstract
{
    public interface IArticleRepository:IEntityRepository<Article>
    {
       
    }
}
