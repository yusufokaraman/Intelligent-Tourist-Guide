using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Shared.Utilities.Results.ComplexTypes
{
    /// <summary>
    /// Enum yapıları enumaration olarak ifade edilir ve numalarandırma amacıyla kullanılır. O sebepten burada class yerine enum kullanmaktayım.
    /// </summary>
    public enum ResultStatus
    {
        Success=0,
        Error=1,
        Warning=2,//ResultStatus.Warning olarak kullanılacak fakat aslında arkaplanda bir sayı olarak tutulacak. Bu sebepten enum yapısını kullanıyoruz da diyebilirim.
        Info=3
    }
}
