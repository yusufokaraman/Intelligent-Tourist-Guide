using ITG.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Shared.Utilities.Results.Abstract
{/// <summary>
/// Kullanıcıya bir result yani sonuç döneceğimden bu result ın durumunu kulllanıcı ile paylaşmam gerekmektedir. Durumdan öte bahsedilen yani bu result hatalı mı başarılı mı 
/// yoksa sadece bir infodan mı oluşuyor. Bunu bir status bir durum olarak ifade ediypr olacağım.
/// Son olarak ise bu propertylerin sadece get alanları olmalı,ctr içinde bu bilgileri veriyor olacağım. Ancak daha sonrasında değiştirebilir olmayacak.    
/// </summary>
    public interface IResult
    {
        public ResultStatus ResultStatus { get;  }//Bu alanda kullanım alanı gösterilecektir. ResultStatus.Success //ResultStatus.Error
        public string Message { get;  }
        //Service katmanında bir exception taşımam gerekebileceğimden Exception oluşturuyorum.
        public Exception Exception { get;  }
    }
}
