using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Tantowi.Json;

namespace Tantowi.Json.Test
{
    [TestFixture]
    class JsonMapTest
    {
        [SetUp]
        protected void SetUp()
        {
        }


        [Test]
        public void ToStringTest1()
        {
            JsonMap map = JsonValue.NewMap();
            map.Put("nama", "Tantowi\rMustofa");
            map.Put("umur", 52);
            map.Put("active", true);
            map.Put("windows", "d:\\data\\radio");
            map.Put("linux", "/var/data/radio");

            File.WriteAllText("d:\\test.json", map.ToString());

            string str = map.ToString();
            Assert.AreEqual(str, "{\"nama\":\"Tantowi\\rMustofa\",\"umur\":52,\"active\":true,\"windows\":\"d:\\\\data\\\\radio\",\"linux\":\"/var/data/radio\"}");
        }

        [Test]
        public void ParseTest1()
        {
            string str = "{\"nama\":\"Tantowi\\rMustofa\",\"umur\":52,\"active\":true,\"windows\":\"d:\\\\data\\\\radio\",\"linux\":\"/var/data/radio\"}";
            JsonValue json = JsonValue.Parse(str);

            Assert.IsTrue(json.IsMap());

            JsonMap map = json.GetMap();
            string nama = map.GetString("nama");
            int umur = (int) map.GetLong("umur");
            bool active = map.GetBoolean("active");
            string windows = map.GetString("windows");
            string linux = map.GetString("linux");

            Assert.AreEqual(nama, "Tantowi\rMustofa");
            Assert.AreEqual(umur, 52);
            Assert.IsTrue(active);
            Assert.AreEqual(windows, "d:\\data\\radio");
            Assert.AreEqual(linux, "/var/data/radio");
        }
    }
}
