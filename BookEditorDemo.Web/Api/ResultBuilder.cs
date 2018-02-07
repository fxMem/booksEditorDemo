using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookEditorDemo.Web.Api
{
    public class ResultBuilder
    {
        public ResultCode Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public ResultBuilder(ResultCode code)
        {
            Code = code;
        }

        public static ResultBuilder Success()
        {
            return new ResultBuilder(ResultCode.Success);
        }

        public static ResultBuilder Error()
        {
            return new ResultBuilder(ResultCode.InternalError);
        }

        public ResultBuilder WithMessage(string message)
        {
            Message = message;
            return this;
        }

        public ResultBuilder WithData(object data)
        {
            Data = data;
            return this;
        }

        public ApiResult Build()
        {
            return new ApiResult { Code = Code, Message = Message, Data = Data };
        }

        public static implicit operator ApiResult(ResultBuilder builder)
        {
            return builder.Build();
        }
            

    }
}