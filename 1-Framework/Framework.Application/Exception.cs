using Framework.Application.Common.Extentions;
using Framework.Domain.Enum;
using System;

namespace Framework.Application
{
    public class ExceptionResult : Exception
    {
        public StatusEnum StatusEnum { get; set; }
        public string Message { get; set; }


        public ExceptionResult(ExceptionResult exception)
        {
            Message = exception.Message;
        }
        public ExceptionResult(StatusEnum Code,string message)
        {
            StatusEnum = Code;
            Message = message;
        }

        public ExceptionResult(StatusEnum Code)
        {
            StatusEnum = Code;
            Message = EnumHelper.GetDisplayName(Code);
        }

        
    }
}
