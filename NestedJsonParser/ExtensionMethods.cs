using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NestedJsonParser
{
    public static class ExtensionMethods
    {
        public static event EventHandler<ParseError> ErrorOccured;

        /// <summary>
        /// convert JSON-string (with nested objects) to nested Key-Value-List
        /// </summary>
        /// <param name="json">json-parsed string</param>
        /// <returns></returns>
        public static IEnumerable<RawValue> ToRawValueList(this string json)
        {
            try
            {
                var obj = JObject.Parse(json);
                return obj.ToRawValueList();
            }
            catch (Exception ex)
            {
                ErrorOccured?.Invoke(null, new ParseError
                {
                    ErrorMessage = string.Concat("Error parsing Json-string!"),
                    Exception = ex
                });

                return null;
            }
        }

        /// <summary>
        /// convert JSON-object (with nested objects) to nested Key-Value-List
        /// </summary>
        /// <param name="json">json-object</param>
        /// <returns></returns>
        public static IEnumerable<RawValue> ToRawValueList(this JObject json)
        {
            try
            {
                Action<RawValue> SetChildren = null;
                SetChildren = parent =>
                {
                    if (parent.ValueType == JTokenType.Object)
                    {
                        var pvlChilds = new List<RawValue>();

                        foreach (var element in (JObject)parent.Value)
                        {
                            if (parent.ValueType == JTokenType.Object)
                            {
                                RawValue pvi = new RawValue(element.Key, element.Value);

                                pvlChilds.Add(pvi);
                            }
                            else
                            {
                            }
                        }
                        parent.Childs = pvlChilds;
                        //Recursively call the SetChildren method for each child.
                        parent.Childs.ForEach(SetChildren);
                        parent.Value = null;
                    }
                    else
                    {
                        if (parent.ValueType == JTokenType.Array)
                        {
                            var pvlChilds = new List<RawValue>();

                            foreach (var elementObj in ((JArray)parent.Value))
                            {
                                if (elementObj.Type == JTokenType.Object)
                                {
                                    foreach (var element in (JObject)elementObj)
                                    {
                                        if (element.Value.Type == JTokenType.Object)
                                        {
                                            RawValue pvi = new RawValue(element.Key, element.Value);
                                            pvlChilds.Add(pvi);
                                        }
                                        else
                                        {
                                            RawValue pvi = new RawValue(element.Key, element.Value);
                                            pvlChilds.Add(pvi);
                                        }
                                    }
                                    parent.Childs = pvlChilds;
                                    //Recursively call the SetChildren method for each child.
                                    parent.Childs.ForEach(SetChildren);
                                    parent.Value = null;
                                }
                                else
                                {
                                    parent.Value = elementObj;
                                }
                            }
                        }
                    }
                };

                List<RawValue> pvis = new List<RawValue>();

                foreach (var element in json)
                {
                    var pv = new RawValue(element.Key, element.Value);
                    pvis.Add(pv);
                }

                pvis.ForEach(SetChildren);

                return pvis;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// convert JSON-object (with nested objects) to flat list
        /// </summary>
        /// <param name="json">json-object</param>
        /// <returns></returns>
        public static List<SimpleRawValue> ToRawValueFlatList(this JObject jobject)
        {
            var flat = jobject.Descendants().Where(j => j.Children().Count() == 0).Aggregate(
                    new Dictionary<string, JToken>(),
                    (props, jtoken) =>
                    {
                        props.Add(jtoken.Path, jtoken);
                        return props;
                    });

            var ret = new List<SimpleRawValue>();
            flat.ToList().ForEach(x => ret.Add(new SimpleRawValue( x.Value)));

            return ret;
        }

        /// <summary>
        /// convert JSON-string (with nested objects) to flat list
        /// </summary>
        /// <param name="json">json-parsed string</param>
        /// <returns></returns>
        public static List<SimpleRawValue> ToRawValueFlatList(this string json)
        {
            try
            {
                var obj = JObject.Parse(json);
                return obj.ToRawValueFlatList();
            }
            catch (Exception ex)
            {
                ErrorOccured?.Invoke(null, new ParseError
                {
                    ErrorMessage = string.Concat("Error parsing Json-string!"),
                    Exception = ex
                });

                return null;
            }
        }
    }
}