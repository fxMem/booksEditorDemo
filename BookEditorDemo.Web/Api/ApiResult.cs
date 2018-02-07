using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace BookEditorDemo.Web.Api
{
    public enum ResultCode
    {
        Success = 0,
        InternalError,
    }

    public class ApiResult 
    {
        public ResultCode Code { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }
        
    }
}