using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helper
{
    public class ResultProcessing<GetData> where GetData : class
    {
        public string Message { get; set; }
        public bool IsSccess { get; set; }
        public GetData Data { get; set; }
    }
}

