using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Model
{
    public class ContactResponseModel<T>
    {

        public string Success { get; set; } = "True";
        public string Message { get; set; } = "";
        public T Data { get; set; }
    }
}
