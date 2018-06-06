using Newtonsoft.Json.Linq;

namespace NestedJsonParser
{
    public class SimpleRawValue
    {
        public SimpleRawValue(JToken value)
        {
            ValueType = value.Type;
            Path = value.Path;
            Value = value;
        }

        public string Path { get; set; }

        public JTokenType ValueType { get; set; }

        public JToken Value;
    }
}