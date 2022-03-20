using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.Mapper
{
    public class ApiResponse<Type>
    {
        public string code { get; set; }
        public string status { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public Type data { get; set; }

    }
}
