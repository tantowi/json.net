using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Tantowi.Json
{
    class JsonParser
    {
        private char[] buff = null;
        private int pos = 0;

        /**
         * create empty JsonParser
         */
        private JsonParser()
        {
        }

        /**
         * parse String of JSON data
         * @param buff json data
         * @return JsonValue of the data
         */
        public static JsonValue Parse(String buff)
        {
            JsonParser parser = new JsonParser();
            return parser.DoParse(buff);
        }

        /**
         * parse
         * @param buff
         * @return 
         */
        private JsonValue DoParse(String buff)
        {
            return DoParse(buff.ToCharArray());
        }

        /**
         * parse
         * @param buff
         */
        private JsonValue DoParse(char[] buff)
        {
            this.buff = buff;
            this.pos = 0;

            if (buff.Length == 0) return JsonValue.NULL();
            return ParseValue();
        }

        /**
         * parseValue
         * @return 
         */
        private JsonValue ParseValue()
        {
            if (buff[pos] == '[') return JsonValue.Of(ParseArray());
            else if (buff[pos] == '{') return JsonValue.Of(ParseMap());
            else if (buff[pos] == '"') return JsonValue.Of(ParseString());

            StringBuilder sb = new StringBuilder();
            while (pos < buff.Length && buff[pos] != ',' && buff[pos] != ':' && buff[pos] != ']' && buff[pos] != '}')
            {
                sb.Append(buff[pos]);
                pos++;
            }
            String st = sb.ToString();

            try
            {
                if (st=="null") return JsonValue.NULL();
                if (st=="true") return JsonValue.Of(true);
                if (st=="false") return JsonValue.Of(false);
                if (st.IndexOf('.') >= 0 || st.IndexOf('e') >= 0 || st.IndexOf('E') >= 0) return JsonValue.Of(Convert.ToDouble(st));
                return JsonValue.Of(Convert.ToInt64(st));
            }
            catch (Exception)
            {
                return JsonValue.NULL();
            }
        }

        /**
         * parseArray
         * @return 
         */
        private JsonArray ParseArray()
        {
            if (buff[pos] != '[') return null;

            JsonArray ja = JsonArray.Create();
            while (pos < buff.Length && buff[pos] != ']')
            {
                pos++;   // skip [ or ,
                JsonValue value = ParseValue();
                ja.Add(value);
            }
            pos++;   // skip ]
            return ja;
        }

        /**
         * parseMap
         * @return 
         */
        private JsonMap ParseMap()
        {
            if (buff[pos] != '{') return null;

            JsonMap jo = JsonMap.Create();
            while (pos < buff.Length && buff[pos] != '}')
            {
                pos++;   // skip { or ,
                String name = ParseString();
                pos++;   // skip :
                JsonValue value = ParseValue();
                jo.Put(name, value);
            }
            pos++;   // skip }
            return jo;
        }

        /**
         * parseString
         * @return 
         */
        private String ParseString()
        {
            if (buff[pos] != '"') return "";
            pos++;   // skip "

            StringBuilder sb = new StringBuilder();
            while (pos < buff.Length && buff[pos] != '"')
            {
                char ch = buff[pos];
                if (ch == '\\')
                {
                    pos++;
                    if (pos >= buff.Length) continue;

                    char ch2 = buff[pos];
                    if (ch2 == '\\') sb.Append('\\');
                    else if (ch2 == '"') sb.Append('"');
                    else if (ch2 == '/') sb.Append('/');
                    else if (ch2 == 'b') sb.Append("\b");
                    else if (ch2 == 'f') sb.Append("\f");
                    else if (ch2 == 'n') sb.Append("\n");
                    else if (ch2 == 'r') sb.Append("\r");
                    else if (ch2 == 't') sb.Append("\t");
                    else if (ch2 == 'u')
                    {
                        if (pos + 4 < buff.Length)
                        {
                            try
                            {
                                StringBuilder sc = new StringBuilder();
                                sc.Append(buff[pos + 1]);
                                sc.Append(buff[pos + 2]);
                                sc.Append(buff[pos + 3]);
                                sc.Append(buff[pos + 4]);
                                char scc = (char) Convert.ToInt32(sc.ToString(), 16);
                                sb.Append(scc);
                            }
                            catch (Exception)
                            {
                            }
                            pos += 4;
                        }
                        else pos = buff.Length - 1;
                    }
                }
                else sb.Append(ch);

                pos++;  // next
            }
            pos++;  // skip "
            return sb.ToString();
        }

    }
}