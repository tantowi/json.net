using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Tantowi.Json
{
    public class JsonArray
    {
        private readonly List<JsonValue> items;

        /**
         * create empty Json Array
         */
        private JsonArray()
        {
            this.items = new List<JsonValue>();
        }

        /**
         * create new JsonArray
         * @return 
         */
        public static JsonArray Create()
        {
            return new JsonArray();
        }

        /**
         * Add a value to array
         * @param value 
         */
        public void Add(JsonValue value)
        {
            items.Add(value);
        }

        /**
         * Add a value to array
         * @param value 
         */
        public void Add(long value)
        {
            items.Add(JsonValue.Of(value));
        }

        /**
         * Add a value to array
         * @param value 
         */
        public void Add(double value)
        {
            items.Add(JsonValue.Of(value));
        }

        /**
         * Add a value to array
         * @param value 
         */
        public void Add(bool value)
        {
            items.Add(JsonValue.Of(value));
        }

        /**
         * Add a value to array
         * @param value 
         */
        public void Add(string value)
        {
            items.Add(JsonValue.Of(value));
        }

        /**
         * Add a value to array
         * @param value 
         */
        public void Add(JsonMap value)
        {
            items.Add(JsonValue.Of(value));
        }

        /**
         * Add a value to array
         * @param value 
         */
        public void Add(JsonArray value)
        {
            items.Add(JsonValue.Of(value));
        }

        /**
         * Add a null value to array
         */
        public void AddNull()
        {
            items.Add(JsonValue.NULL());
        }

        /**
         * get a value
         * @param i index 
         * @return 
         */
        public JsonValue Get(int i)
        {
            return items[i];
        }

        /// <summary>
        /// Get value as String
        /// </summary>
        /// <param name="i">index</param>
        /// <returns></returns>
        public string GetString(int i)
        {
            JsonValue value = items[i];
            if (value is null) throw new ArgumentNullException(i.ToString());
            return value.GetString();
        }

        /// <summary>
        /// Get value as long
        /// </summary>
        /// <param name="i">index</param>
        /// <returns></returns>
        public long GetLong(int i)
        {
            JsonValue value = items[i];
            if (value is null) throw new ArgumentNullException(i.ToString());
            return value.GetLong();
        }

        /// <summary>
        /// Get value as Boolean
        /// </summary>
        /// <param name="i">index</param>
        /// <returns></returns>
        public bool GetBoolean(int i)
        {
            JsonValue value = items[i];
            if (value is null) throw new ArgumentNullException(i.ToString());
            return value.GetBoolean();
        }

        /// <summary>
        /// Get value as JsonMap
        /// </summary>
        /// <param name="i">index</param>
        /// <returns></returns>
        public JsonMap GetMap(int i)
        {
            JsonValue value = items[i];
            if (value is null) throw new ArgumentNullException(i.ToString());
            return value.GetMap();
        }

        /// <summary>
        /// Get value as JsonArray
        /// </summary>
        /// <param name="i">index</param>
        /// <returns></returns>
        public JsonArray GetArray(int i)
        {
            JsonValue value = items[i];
            if (value is null) throw new ArgumentNullException(i.ToString());
            return value.GetArray();
        }


        /**
         * get the size of the array
         * @return 
         */
        public int Size
        {
            get { return items.Count; }
        }

        /**
         * convert the arry to json array string
         * @return 
         */
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            BuildString(sb);
            return sb.ToString();
        }

        /**
         * build a json string
         * @param sb 
         */
        public void BuildString(StringBuilder sb)
        {
            sb.Append('[');
            int sz = items.Count;
            for (int i = 0; i < sz; i++)
            {
                if (i > 0) sb.Append(',');
                JsonValue item = items[i];
                item.BuildString(sb);
            }
            sb.Append(']');
        }

    }
}
