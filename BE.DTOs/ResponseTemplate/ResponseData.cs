using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DTOs.ResponseTemplate
{
    public class ResponseData<T>
    {
        public T? Data { get; set; }
        public int Code { get; set; } = 200;
        public string Message { get; set; } = "success";
    }
}
