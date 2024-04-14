using System.Collections.Generic;

namespace ChatFrontend.Classes
{
    class ErrorResponse
    {
        public string Detail { get; set; }
    }
    class UnprocessableEntity
    {
        public List<ErrorDetail> Detail { get; set; }
    }

    class ErrorDetail
    {
        public string Type { get; set; }
        public List<string> Loc { get; set; }
        public string Msg { get; set; }
        public object Input { get; set; }
        public Dictionary<string, object> Ctx { get; set; }
        public string Url { get; set; }
    }
}
