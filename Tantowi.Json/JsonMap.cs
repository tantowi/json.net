using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Tantowi.Json
{

    /// <summary>
    /// JsonMap
    /// </summary>
    public class JsonMap
    {
        private readonly Dictionary<string, JsonValue> items;


        /// <summary>
        /// Create empty JsonMap
        /// </summary>
        private JsonMap()
        {
            this.items = new Dictionary<string, JsonValue>();
        }

        /// <summary>
        /// Create empty JsonMap
        /// </summary>
        /// <returns></returns>
        public static JsonMap Create()
        {
            return new JsonMap();
        }

        /**
         * Put a value to Json Map
         * @param name name
         * @param value value
         */
        public void Put(string name, JsonValue value)
        {
            items.Add(name.Trim(), value);
        }

        /**
         * Put a value to Json Map
         * @param name name
         * @param value value
         */
        public void Put(string name, long value)
        {
            items.Add(name.Trim(), JsonValue.Of(value));
        }

        /**
         * Put a value to Json Map
         * @param name name
         * @param value value
         */
        public void Put(string name, double value)
        {
            items.Add(name.Trim(), JsonValue.Of(value));
        }

        /**
         * Put a value to Json Map
         * @param name name
         * @param value value
         */
        public void Put(string name, bool value)
        {
            items.Add(name.Trim(), JsonValue.Of(value));
        }

        /**
         * Put a value to Json Map
         * @param name name
         * @param value value
         */
        public void Put(string name, string value)
        {
            items.Add(name.Trim(), JsonValue.Of(value));
        }

        /**
         * Put a value to Json Map
         * @param name name
         * @param value value
         */
        public void Put(string name, JsonMap value)
        {
            items.Add(name.Trim(), JsonValue.Of(value));
        }

        /**
         * Put a value to Json Map
         * @param name name
         * @param value value
         */
        public void Put(string name, JsonArray value)
        {
            items.Add(name.Trim(), JsonValue.Of(value));
        }

        /**
         * Put a null value to Json Map
         * @param name name
         */
        public void PutNull(string name)
        {
            items.Add(name.Trim(), JsonValue.NULL());
        }

        /**
         * Returns the value for the key, or null if the key not found
         * @param name the key
         * @return JsonValue, or null if the key not found
         */
        public JsonValue Get(string name)
        {
            return items[name];
        }

        /// <summary>
        /// Get value as string
        /// </summary>
        /// <param name="key">key name</param>
        /// <returns>String value</returns>
        public string GetString(string key)
        {
            JsonValue value = items[key];
            if (value is null) throw new ArgumentNullException(nameof(key));
            return value.GetString();
        }

        /// <summary>
        /// Get value as long
        /// </summary>
        /// <param name="key">key name</param>
        /// <returns></returns>
        public long GetLong(string key)
        {
            JsonValue value = items[key];
            if (value is null) throw new ArgumentNullException(nameof(key));
            return value.GetLong();
        }

        /// <summary>
        /// Get value as Boolean
        /// </summary>
        /// <param name="key">key name</param>
        /// <returns></returns>
        public bool GetBoolean(string key)
        {
            JsonValue value = items[key];
            if (value is null) throw new ArgumentNullException(nameof(key));
            return value.GetBoolean();
        }

        /// <summary>
        /// Get value as JsonMap
        /// </summary>
        /// <param name="key">key name</param>
        /// <returns></returns>
        public JsonMap GetMap(string key)
        {
            JsonValue value = items[key];
            if (value is null) throw new ArgumentNullException(nameof(key));
            return value.GetMap();
        }

        /// <summary>
        /// Get value as JsonArray
        /// </summary>
        /// <param name="key">key name</param>
        /// <returns></returns>
        public JsonArray GetArray(string key)
        {
            JsonValue value = items[key];
            if (value is null) throw new ArgumentNullException(nameof(key));
            return value.GetArray();
        }


        /// <summary>
        /// get number of items in the map 
        /// </summary>
        /// <returns></returns>
        public int Size
        {
            get { return items.Count; }
        }

        /// <summary>
        /// get list of keys on the map
        /// </summary>
        /// <returns></returns>
        public string[] GetKeys()
        {
            List<string> keys = new List<string>();

            Dictionary<string, JsonValue>.KeyCollection keyColl = items.Keys;
            foreach (string st in keyColl)
            {
                keys.Add(st);
            }
            return keys.ToArray();
        }

        /// <summary>
        /// Convert to string
        /// </summary>
        /// <returns></returns>
       public override string ToString()
       {
            StringBuilder sb = new StringBuilder();
            BuildString(sb);
            return sb.ToString();
       }

        /// <summary>
        /// Build json string to a StringBuilder
        /// </summary>
        /// <param name="sb"></param>
        public void BuildString(StringBuilder sb)
        {
            bool first = true;
            sb.Append("{");

            foreach (KeyValuePair<string, JsonValue> item in items)
            {
                if (first) first = false;
                else sb.Append(",");

                sb.Append("\"");
                sb.Append(item.Key);
                sb.Append("\":");
                JsonValue value = item.Value;
                value.BuildString(sb);
            }
            sb.Append("}");
        }

    }
}