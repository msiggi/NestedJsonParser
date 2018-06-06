using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace NestedJsonParser
{
    public class RawValue
    {
        public RawValue(string name,  JToken value)
        {
            Name = name;
            ValueType = value.Type;
            Path = value.Path;
            Value = value;
        }

        public List<RawValue> Childs { get; set; } = new List<RawValue>();

        public string Name { get; set; }
        public string Path { get; set; }

        public JTokenType ValueType { get; set; }


        public object Value;
    }
}
