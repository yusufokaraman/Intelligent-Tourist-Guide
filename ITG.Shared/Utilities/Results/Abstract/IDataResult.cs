using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Shared.Utilities.Results.Abstract
{
    /// <summary>
    /// Bir Kategori Liste ve User gibi entityleri getirmek isteyebileceğimizden burası generic olmalıdır. 
    /// Burada IList değil de IEnumarable olarak da kullanabiliriz. Bu farklılıkları birlikte kullanabilmek için ise out T olarak bir tanımlama yapılmıştır.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataResult<out T>:IResult
    {
        public T Data { get;  }// new DataResult<Category>(ResultStatus.Success, category);
                                //Liste halinde göndermek istersek ise  new DataResult<IList<Category>>(Result.Status.Success,category);
    }
}
