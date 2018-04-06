using System;

namespace NestedJsonParser
{
    public class ParseError : EventArgs
    {
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }
    }
}