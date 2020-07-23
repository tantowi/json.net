using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Tantowi.Json
{
    public class JsonValue
    {
        private Object value;

        /**
         * Create JsonValue object
         */
        private JsonValue()
        {
        }

        /**
         * Crate JsonValue from a long value
         * @param value
         * @return 
         */
        public static JsonValue Of(long value)
        {
            JsonValue v = new JsonValue
            {
                value = value
            };
            return v;
        }

        /**
         * Create JsonValue from a double value
         * @param value
         * @return 
         */
        public static JsonValue Of(double value)
        {
            JsonValue v = new JsonValue();
            if (Double.IsNaN(value) || Double.IsInfinity(value)) v.value = null;
            else v.value = value;

            return v;
        }

        /**
         * Create JsonValue from a bool value
         * @param value
         * @return 
         */
        public static JsonValue Of(bool value)
        {
            JsonValue v = new JsonValue
            {
                value = value
            };
            return v;
        }

        /**
         * Crate JsonValue from a string value
         * @param value
         * @return 
         */
        public static JsonValue Of(string value)
        {
            JsonValue v = new JsonValue
            {
                value = value
            };
            return v;
        }

        /**
         * Create JsonValue from a JsonArray array
         * @param value
         * @return 
         */
        public static JsonValue Of(JsonArray value)
        {
            JsonValue v = new JsonValue
            {
                value = value
            };
            return v;
        }

        /**
         * Create JsonValue from a JsonMap object
         * @param value
         * @return 
         */
        public static JsonValue Of(JsonMap value)
        {
            JsonValue v = new JsonValue
            {
                value = value
            };
            return v;
        }

        /**
         * Crate a NULL JsonValue
         * @return 
         */
        public static JsonValue NULL()
        {
            JsonValue v = new JsonValue
            {
                value = null
            };
            return v;
        }

        /**
         * Create a JsonMap object
         * @return 
         */
        public static JsonMap NewMap()
        {
            return JsonMap.Create();
        }

        /**
         * Create a JsonArray object
         * @return 
         */
        public static JsonArray NewArray()
        {
            return JsonArray.Create();
        }

        /**
         * Parse a json string to JsonValue
         * @param json
         * @return 
         */
        public static JsonValue Parse(string json)
        {
            return JsonParser.Parse(json);
        }

        /**
         * Get value of data stored
         * @return 
         */
        public Object GetValue()
        {
            return this.value;
        }


        /**
         * Whether this value is null
         * @return 
         */
        public bool IsNull()
        {
            return (this.value == null);
        }

        /**
         * Whether this value is a long
         * @return 
         */
        public bool IsLong()
        {
            return (this.value is long);
        }

        /**
         * Whether this value is a double
         * @return 
         */
        public bool IsDouble()
        {
            return (this.value is Double);
        }

        /**
         * Whether this value is a string
         * @return 
         */
        public bool IsString()
        {
            return (this.value is String);
        }

        /**
         * Whether this value is a bool
         * @return 
         */
        public bool IsBoolean()
        {
            return (this.value is bool);
        }

        /**
         * Whether this value is a map (json object)
         * @return 
         */
        public bool IsMap()
        {
            return (this.value is JsonMap);
        }

        /**
         * Whether this value is an array (json array)
         * @return 
         */
        public bool IsArray()
        {
            return (this.value is JsonArray);
        }

        /**
         * Get Long value
         * @return 
         */
        public long GetLong()
        {
            if (!(value is long)) throw new Exception("Not a long value (" + value.GetType().ToString() + ")");
            return (long)value;
        }

        /**
         * Get Double value
         * @return 
         */
        public double GetDouble()
        {
            if (!(value is Double)) throw new Exception("Not a double value (" + value.GetType().ToString() + ")");
            return (double)value;
        }

        /**
         * Get string value
         * @return 
         */
        public string GetString()
        {
            if (!(value is string)) throw new Exception("Not a string value (" + value.GetType().ToString() + ")");
            return (string)value;
        }

        /**
         * Get bool value
         * @return 
         */
        public bool GetBoolean()
        {
            if (!(value is bool)) throw new Exception("Not a boolean value (" + value.GetType().ToString() + ")");
            return (bool)value;
        }

        /**
         * Get Map value
         * @return 
         */
        public JsonMap GetMap()
        {
            if (!(value is JsonMap)) throw new Exception("Not a map value (" + value.GetType().ToString() + ")");
            else return (JsonMap)value;
        }

        /**
         * Get Array value
         * @return 
         */
        public JsonArray GetArray()
        {
            if (!(value is JsonArray)) throw new Exception("Not an array value (" + value.GetType().ToString() + ")");
            else return (JsonArray)value;
        }

        /**
         * convert the value to json string
         * @return 
         */
       public override string ToString()
       {
           StringBuilder sb = new StringBuilder();
            this.BuildString(sb);
            return sb.ToString();
       }

        /**
         * buildString
         * @param sb 
         */
        public void BuildString(StringBuilder sb)
        {
            if (value == null) sb.Append("null");
            else if (value is long) sb.Append(value.ToString());
            else if (value is double @double) @double.ToString("#.####");
            else if (value is bool boolean) sb.Append(boolean ? "true" : "false");
            else if (value is string @string) EscapeStr(sb, @string);
            else if (value is JsonArray array) array.BuildString(sb);
            else if (value is JsonMap map) map.BuildString(sb);
        }

        /**
         * Reference: 
         * http://www.json.org/
         * http://www.unicode.org/versions/Unicode5.1.0/
         * @param value
         * @return 
         */

        public static string EscapeStr(string value)
        {
            StringBuilder sb = new StringBuilder();
            EscapeStr(sb, value);
            return sb.ToString();
        }

        /**
         * Escape the value and append to StringBuilder
         * @param sb
         * @param value 
         */
        private static void EscapeStr(StringBuilder sb, string value)
        {
            sb.Append('"');
            int len = value.Length;
            for (int i = 0; i < len; i++)
            {
                char ch = value[i];
                if (ch == '"') sb.Append("\\\"");
                else if (ch == '\\') sb.Append("\\\\");
                else if (ch == '\b') sb.Append("\\b");
                else if (ch == '\f') sb.Append("\\f");
                else if (ch == '\n') sb.Append("\\n");
                else if (ch == '\r') sb.Append("\\r");
                else if (ch == '\t') sb.Append("\\t");
                else if ((ch >= '\u0000' && ch <= '\u001F') || (ch >= '\u007F' && ch <= '\u009F') || (ch >= '\u2000' && ch <= '\u20FF'))
                {
                    int ich = ch;
                    sb.Append("\\u");
                    sb.Append(ich.ToString("X4"));
                }
                else sb.Append(ch);
            }
            sb.Append('"');
        }

    }
}