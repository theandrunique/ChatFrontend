using System;
using System.Collections.Generic;

namespace ChatFrontend.Models
{
    public class ErrorResponse : Exception
    {
        public string Detail { get; set; }
    }
    public class UnprocessableEntity : Exception
    {
        public List<ErrorDetail> Detail { get; set; }
    }

    public class ErrorDetail
    {
        public string Type { get; set; }
        public List<string> Loc { get; set; }
        public string Msg { get; set; }
        public object Input { get; set; }
        public Dictionary<string, object> Ctx { get; set; }
        public string Url { get; set; }
    }
}
